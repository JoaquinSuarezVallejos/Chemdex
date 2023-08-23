﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class ShellLvl1 : MonoBehaviour
    {
        [SerializeField] private float particleSpeed; // how fast the particles move
        [SerializeField] private float orbitSpeed; // how fast the particles orbit

        private List<ParticleLvl1> particles; // list of all the particles in this shell
        private float seperationDistance; // how far apart each electron should be
        private float scale = 1;
        private float radius;

        public float Radius // radius of the shell
        {
            get { return radius; } // get the radius
            set
            {
                radius = value;
                CalcSeperationDistance(); // calculate the separation distance
            }
        } // desired orbital radius

        public int ElectronCount { get { return particles.Count; } } // number of electrons in this shell
        public ShellLvl1 NextShell { get; set; } // the next shell in the atom
        public int MaxParticles { get; set; } // the maximum number of particles in this shell
        public bool Full { get { return ElectronCount == MaxParticles; } } // true if the shell is full
        public bool Empty { get { return ElectronCount == 0; } } // true if the shell is empty
        public ParticleLvl1[] Particles { get { return particles.ToArray(); } } // array of all the particles in this shell

        public float Scale // scale of the shell
        {
            set
            {
                scale = value;
                foreach (ElectronLvl1 particle in particles) // set the scale of each particle
                {
                    particle.Radius = scale / 4; // set the radius of the particle
                }
            }
        }

        private void Awake()
        {
            particles = new List<ParticleLvl1>(); // create a new list of particles
        }

        /// <summary>
        /// Add a particle to this shell
        /// </summary>
        /// <param name="particle">Particle to be added</param>
        /// <returns>true if sucessfully added</returns>
        public bool AddParticle(ParticleLvl1 particle)
        {
            if(NextShell != null && !NextShell.Full)
            {
                return NextShell.AddParticle(particle);
            }
            //make sure the particle is an electron and the shell is not full
            else if (particle.GetType().Equals(typeof(Electron)) && !Full )
            {
                //add the particle
                particles.Add(particle);
                particle.transform.SetParent(transform);
                particle.Radius = scale / 4;

                //calculate the new seperation distance
                CalcSeperationDistance();
                return true;
            }
            return false;
        }

        public bool RemoveParticle(ParticleLvl1 particle) // remove a particle from this shell
        {
            // make sure the particle is an electron and is actually in this shell
            if (particle.GetType().Equals(typeof(ElectronLvl1)) && particles.Contains(particle))
            {
                particles.Remove(particle); // remove the particle
                particle.transform.SetParent(null); // remove the parent

                // calculate the new separation distance
                CalcSeperationDistance();
                return true;
            }

            // not in shell, check the next one
            else if (NextShell != null) 
            {
                // recursively check if the particle is in the next shell
                if (NextShell.RemoveParticle(particle))
                {
                    if (particles.Count > 0) // if there are particles in this shell
                    {
                        // replace the removed partcicle with one from this shell
                        ParticleLvl1 transferParticle = particles[0];
                        particles.Remove(transferParticle);
                        NextShell.AddParticle(transferParticle);

                        CalcSeperationDistance();
                    }
                    return true;
                }
            }
            return false;
        }

        private void FixedUpdate()
        {
            foreach (ParticleLvl1 particle in particles)
            {
                //calculate force to get into orbit
                Vector3 diffRadius = transform.position - particle.PhysicsObj.Position;
                Vector2 forceToRadius = diffRadius.normalized * (diffRadius.magnitude - Radius) * particleSpeed;

                //calculate force to maintain orbit
                Vector2 forceToOrbit = new Vector2(-diffRadius.y, diffRadius.x).normalized * orbitSpeed * scale;

                //calculate the force to seperate
                Vector2 forceToSeperate = Vector3.zero;
                foreach (ParticleLvl1 other in particles)
                {
                    //don't seperate from self
                    if (!particle.Equals(other))
                    {
                        //find the distance between particles
                        Vector2 diffOther = particle.PhysicsObj.Position - other.PhysicsObj.Position;

                        //rare occurance, but seperate from identical other
                        if (diffOther.sqrMagnitude < 0.01)
                        {
                            forceToSeperate = Random.insideUnitSphere;
                        }
                        else
                        {
                            //calculate the amount of overlap
                            float overlap = diffOther.magnitude - seperationDistance;

                            if (overlap < 0)
                            {
                                //add force to seperate
                                forceToSeperate -= diffOther.normalized * overlap;
                            }
                        }
                    }
                }
                forceToSeperate *= particleSpeed;

                //apply forces to the particles
                particle.PhysicsObj.AddForce(forceToRadius + forceToOrbit + forceToSeperate);
            }
        }

        public void TrimElectrons(int num)
        {
            if (num > 0)
            {
                ParticleLvl1[] pA = particles.ToArray(); // copy to array so list can be mutated
                foreach (ParticleLvl1 particle in pA)
                {
                    RemoveParticle(particle);
                    particle.OnDeselect?.Invoke();

                    if (--num <= 0)
                        return;
                }

                //trim remaining from next shell
                NextShell.TrimElectrons(num);
            }
        }

        /// <summary>
        /// calculates the distance between points when n points are equally spaced on peremiter of circle
        /// </summary>
        /// <param name="n">number of points on circle</param>
        /// <returns>distance between points</returns>
        private void CalcSeperationDistance()
        {
            seperationDistance = 2 * Radius * Mathf.Sin(Mathf.PI / particles.Count);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}