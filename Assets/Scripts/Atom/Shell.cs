using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Shell : MonoBehaviour
    {
        [SerializeField] private float particleSpeed; //magnitude of force to get into orbit
        [SerializeField] private float orbitSpeed; //magnitude of orbital force

        private List<Particle> particles; //list of all the particles in this shell
        private float seperationDistance; //how far apart each electron should be
        private float scale = 1;
        private float radius;

        public float Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                CalcSeperationDistance();
            }
        }//desired orbital radius

        public int ElectronCount { get { return particles.Count; } }
        public Shell NextShell { get; set; }
        public int MaxParticles { get; set; }
        public bool Full { get { return ElectronCount == MaxParticles; } }
        public bool Empty { get { return ElectronCount == 0; } }
        public Particle[] Particles { get { return particles.ToArray(); } }
        public float Scale
        {
            set
            {
                scale = value;
                foreach (Electron particle in particles)
                {
                    particle.Radius = scale / 4;
                }
            }
        }


        private void Awake()
        {
            particles = new List<Particle>();
        }

        /// <summary>
        /// Add a particle to this shell
        /// </summary>
        /// <param name="particle">Particle to be added</param>
        /// <returns>true if sucessfully added</returns>
        public bool AddParticle(Particle particle)
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

        /// <summary>
        /// Removes a particle from this shell
        /// </summary>
        /// <param name="particle">Particle to remove</param>
        /// <returns></returns>
        public bool RemoveParticle(Particle particle)
        {
            //make sure the particle is an electron and actually in this shell
            if (particle.GetType().Equals(typeof(Electron)) && particles.Contains(particle))
            {
                particles.Remove(particle);
                particle.transform.SetParent(null);

                //calculate the new seperation distance
                CalcSeperationDistance();
                return true;
            }
            //not in shell, check the next one
            else if (NextShell != null)
            {
                //recursively check if particle in next shell
                if (NextShell.RemoveParticle(particle))
                {
                    if(particles.Count > 0)
                    {
                        //replace the removed partcicle with one from this shell
                        Particle transferParticle = particles[0];
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
            foreach (Particle particle in particles)
            {
                //calculate force to get into orbit
                Vector3 diffRadius = transform.position - particle.PhysicsObj.Position;
                Vector2 forceToRadius = diffRadius.normalized * (diffRadius.magnitude - Radius) * particleSpeed;

                //calculate force to maintain orbit
                Vector2 forceToOrbit = new Vector2(-diffRadius.y, diffRadius.x).normalized * orbitSpeed * scale;

                //calculate the force to seperate
                Vector2 forceToSeperate = Vector3.zero;
                foreach (Particle other in particles)
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
                Particle[] pA = particles.ToArray(); // copy to array so list can be mutated
                foreach (Particle particle in pA)
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
