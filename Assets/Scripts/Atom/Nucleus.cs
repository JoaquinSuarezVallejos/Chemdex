using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Physics;

namespace Atom
{
    [RequireComponent(typeof(PhysicsObject))]
    public class Nucleus : MonoBehaviour
    {
        /// <summary>
        /// Handles the behavior of Atom's nucleus
        /// </summary>

        private const float particleSpeed = 1.2f; //magnitude of force to center
        private const float rotationSpeed = 20; //degees to spin 

        private List<Particle> particles; //list of all particles in nucleus
        private float scale = 1;

        public int ProtonCount { get; private set; } = 0;
        public int NeutronCount { get; private set; } = 0;
        public int Mass { get { return ProtonCount + NeutronCount; } }
        public bool Shake { private get; set; }
        public float Scale
        {
            set
            {
                scale = value;
                foreach (Particle particle in particles)
                {
                    particle.Radius = scale / 2;
                }
            }
        }

        public int MassMax { get; set; }
        public int MassMin { get; set; }

        private PhysicsObject physicsObject;
        private Vector3 origin;

        private void Awake()
        {
            physicsObject = GetComponent<PhysicsObject>();

            particles = new List<Particle>();
        }

        private void Start()
        {
            origin = transform.localPosition; 
        }

        /// <summary>
        /// Adds a particle to the nucleus
        /// </summary>
        /// <param name="particle">Particle to add</param>
        /// <returns>true when particle successfully added</returns>
        public bool AddParticle(Particle particle)
        {
            //check type of particle
            if (particle.GetType().Equals(typeof(Proton)) && ProtonCount < Elements.NumElements)
            {
                ProtonCount++;

                //add the particle and set the parent
                particles.Add(particle);
                particle.transform.SetParent(transform);
                particle.Radius = scale / 2;
                return true;
            }
            else if (particle.GetType().Equals(typeof(Neutron)) && NeutronCount < MassMax - ProtonCount)
            {
                NeutronCount++;

                //add the particle and set the parent
                particles.Add(particle);
                particle.transform.SetParent(transform);
                particle.Radius = scale / 2;
                return true;
            }
            return false;
        }

        /// <summary>
        /// try and remove a particle from the nucleus
        /// </summary>
        /// <param name="particle">particle to remove</param>
        /// <returns>removal suceess</returns>
        public bool RemoveParticle(Particle particle)
        {
            //check type of particle
            if (particle.GetType().Equals(typeof(Proton)) && particles.Contains(particle))
            {
                ProtonCount--;

                //add the particle and set the parent
                particles.Remove(particle);
                particle.transform.SetParent(null);
                return true;
            }
            else if (particle.GetType().Equals(typeof(Neutron)) && particles.Contains(particle))
            {
                NeutronCount--;

                //add the particle and set the parent
                particles.Remove(particle);
                particle.transform.SetParent(null);
                return true;
            }
            return false;
        } 

        public void TrimProtons(int num)
        {
            if (num > 0)
            {
                Particle[] pA = particles.ToArray(); // copy to array so list can be mutated
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

        public void TrimNeutrons()
        {
            int diff = Mass - MassMax;
            TrimNeutrons(diff);
        }

        public void TrimNeutrons(int num)
        {
            if (num > 0)
            {
                Particle[] pA = particles.ToArray(); // copy to array so list can be mutated
                foreach (Particle particle in pA)
                {
                    if (particle.GetType().Equals(typeof(Neutron)))
                    {
                        RemoveParticle(particle);
                        particle.OnDeselect?.Invoke();

                        if (--num <= 0)
                            return;
                    }
                }
            }
        }

        void Update()
        {
            //slowly spin the nucleus
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            Vector3 forceToOrigin = origin - transform.localPosition;
            if (Shake)
            {
                Vector3 forceToShake = Random.insideUnitSphere;
                physicsObject.AddForce(forceToShake + forceToOrigin);
            }
            else
            {
                physicsObject.AddForce(forceToOrigin);
            }

            float m = 0;

            foreach (Particle particle in particles)
            {
                //find the distance from origin
                Vector3 diffOrgin = transform.position - particle.PhysicsObj.Position;
                //calculate the force to center ( clamp is used so particles slow near center
                Vector3 forceToCenter = Vector3.ClampMagnitude(diffOrgin.normalized * (particleSpeed * scale), diffOrgin.magnitude);

                if(diffOrgin.magnitude > m)
                {
                    m = diffOrgin.magnitude;
                }

                //calculate the force to seperate
                Vector3 forceToSeperate = Vector3.zero;
                foreach (Particle other in particles)
                {
                    //don't seperate from self
                    if (!particle.Equals(other))
                    {
                        //find the distance between particles
                        Vector3 diffOther = particle.PhysicsObj.Position - other.PhysicsObj.Position;
                        
                        //rare occurance, but seperate from identical other
                        if (diffOther.sqrMagnitude < 0.01)
                        {
                            forceToSeperate = Random.insideUnitSphere;
                        }
                        else
                        {
                            //calculate the amount of overlap
                            float overlap = diffOther.magnitude - particle.Radius - other.Radius;
                            //check if actually overlapping
                            if (overlap < 0)
                            {
                                //add force to seperate
                                forceToSeperate -= diffOther.normalized * overlap;
                            }
                        }
                    }
                }
                forceToSeperate *= particleSpeed * scale;
                //apply forces to the particles
                particle.PhysicsObj.AddForce(forceToCenter + forceToSeperate);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, Mathf.Log(Mass, 30 / scale) * scale + (scale / 2));
        }
    }
}
