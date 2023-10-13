﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Physics;

namespace Atom
{
    [RequireComponent(typeof(PhysicsObject))]
    public class Nucleus : MonoBehaviour
    {
        /// Summary: handles the behavior of the atom's nucleus

        private const float particleSpeed = 1.2f; // speed of particles 
        private const float rotationSpeed = 20; // speed of rotation of particles

        private List<Particle> particles; // list of all particles in the nucleus
        private float scale = 1; // scale of the nucleus

        public int ProtonCount { get; private set; } = 0; // number of protons in the nucleus
        public int NeutronCount { get; private set; } = 0; // number of neutrons in the nucleus
        public int Mass { get { return ProtonCount + NeutronCount; } } // get the total mass of the nucleus
        public bool Shake { private get; set; } // true when the nucleus is shaking
        public float Scale // scale of the nucleus
        {
            set
            {
                scale = value;
                foreach (Particle particle in particles) // set the scale of all particles
                {
                    particle.Radius = scale / 2; // set the radius of the particle
                }
            }
        }

        // maximum and minimum mass of the nucleus
        public int MassMax { get; set; } 
        public int MassMin { get; set; } 

        private PhysicsObject physicsObject;
        private Vector3 origin;

        private void Awake()
        {
            physicsObject = GetComponent<PhysicsObject>(); // get the physics object
            particles = new List<Particle>(); // create a new list of particles
        }

        private void Start()
        {
            origin = transform.localPosition;  // set the origin of the nucleus
        }

        public bool AddParticle(Particle particle) // add a particle to the nucleus
        {
            // check type of particle
            if (particle.GetType().Equals(typeof(Proton)) && ProtonCount < Elements.NumElements) // check if the particle is a proton and if the nucleus is not full
            {
                ProtonCount++;

                // add the particle and set the parent
                particles.Add(particle);
                particle.transform.SetParent(transform);
                particle.Radius = scale / 2;
                return true;
            }

            else if (particle.GetType().Equals(typeof(Neutron)) && NeutronCount < MassMax - ProtonCount) // check if the particle is a neutron and if the nucleus is not full
            {
                NeutronCount++;

                // add the particle and set the parent
                particles.Add(particle);
                particle.transform.SetParent(transform);
                particle.Radius = scale / 2;
                return true;
            }
            return false;
        }

        public bool RemoveParticle(Particle particle) // remove a particle from the nucleus
        {
            // check type of particle
            if (particle.GetType().Equals(typeof(Proton)) && particles.Contains(particle)) // check if the particle is a proton and if it is in the nucleus
            {
                ProtonCount--;

                // remove the particle (proton) and set the parent
                particles.Remove(particle);
                particle.transform.SetParent(null);
                return true;
            }

            else if (particle.GetType().Equals(typeof(Neutron)) && particles.Contains(particle)) // check if the particle is a neutron and if it is in the nucleus
            {
                NeutronCount--;

                // remove the particle (neutron) and set the parent
                particles.Remove(particle);
                particle.transform.SetParent(null);
                return true;
            }
            return false;
        } 

        public void TrimProtons(int num) // remove protons from the nucleus 
        {
            if (num > 0)
            {
                Particle[] pA = particles.ToArray(); 
                foreach (Particle particle in pA)
                {
                    if (particle.GetType().Equals(typeof(Proton)))
                    {
                        RemoveParticle(particle);
                        particle.OnDeselect?.Invoke();

                        if (--num <= 0)
                            return;
                    }
                }
            }
        }

        public void TrimNeutrons() // remove neutrons from the nucleus
        {
            int diff = Mass - MassMax;
            TrimNeutrons(diff);
        }

        public void TrimNeutrons(int num) // remove neutrons from the nucleus
        {
            if (num > 0) // check if the number of neutrons to remove is greater than 0
            {
                Particle[] pA = particles.ToArray(); // copy to array so list can be mutated
                foreach (Particle particle in pA) // iterate through particles
                {
                    if (particle.GetType().Equals(typeof(Neutron))) // check if particle is a neutron
                    {
                        RemoveParticle(particle); // remove the particle
                        particle.OnDeselect?.Invoke(); // invoke the OnDeselect event

                        if (--num <= 0) // check if the number of neutrons to remove has been reached
                            return;
                    }
                }
            }
        }

        void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // slowly spin/rotate the nucleus
        }

        private void FixedUpdate()
        {
            Vector3 forceToOrigin = origin - transform.localPosition; // calculate the force to the origin

            if (Shake) // check if the nucleus is shaking
            {
                Vector3 forceToShake = Random.insideUnitSphere; // calculate the force to shake
                physicsObject.AddForce(forceToShake + forceToOrigin); // add the force to the nucleus
            }

            else // if the nucleus is not shaking
            {
                physicsObject.AddForce(forceToOrigin);
            }

            float m = 0; // max distance from origin

            foreach (Particle particle in particles) // iterate through particles
            {
                // find the distance from origin
                Vector3 diffOrgin = transform.position - particle.PhysicsObj.Position;
                // calculate the force to center (clamp is used so particles slow near center)
                Vector3 forceToCenter = Vector3.ClampMagnitude(diffOrgin.normalized * (particleSpeed * scale), diffOrgin.magnitude);

                if (diffOrgin.magnitude > m) // check if the distance from origin is greater than the max distance
                {
                    m = diffOrgin.magnitude; // set the max distance
                }

                // calculate the force to separate
                Vector3 forceToSeparate = Vector3.zero;
                
                foreach (Particle other in particles)
                {
                    // don't separate from self
                    if (!particle.Equals(other))
                    {
                        // find the distance between particles
                        Vector3 diffOther = particle.PhysicsObj.Position - other.PhysicsObj.Position;
                        
                        // rare occurance, but separate from identical other
                        if (diffOther.sqrMagnitude < 0.01)
                        {
                            forceToSeparate = Random.insideUnitSphere;
                        }

                        else
                        {
                            // calculate the amount of overlap
                            float overlap = diffOther.magnitude - particle.Radius - other.Radius; // intentar sacar esto del foreach

                            // check if actually overlapping
                            if (overlap < 0)
                            {
                                //add force to separate
                                forceToSeparate -= diffOther.normalized * overlap;
                            }
                        }
                    }
                }

                forceToSeparate *= particleSpeed * scale;
                // apply forces to the particles
                particle.PhysicsObj.AddForce(forceToCenter + forceToSeparate);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, Mathf.Log(Mass, 30 / scale) * scale + (scale / 2));
        }
    }
}