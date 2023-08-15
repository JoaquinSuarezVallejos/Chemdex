/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Physics;

namespace Atom
{
    [RequireComponent(typeof(PhysicsObject))]
    public class CalculateNucleus : MonoBehaviour
    {
        public List<Nucleus> ShakeAtoms = new List<Nucleus>();

        private void FixedUpdate()
        {
            foreach (var nucleus in ShakeAtoms)
            {
                float avg = nucleus.scale / ShakeAtoms.Count;

                Vector3 forceToOrigin = nucleus.origin - transform.localPosition; // calculate the force to the origin

                if (nucleus.Shake) // check if the nucleus is shaking
                {
                    Vector3 forceToShake = Random.insideUnitSphere; // calculate the force to shake
                    nucleus.physicsObject.AddForce(forceToShake + forceToOrigin); // add the force to the nucleus
                }

                else // if the nucleus is not shaking
                {
                    nucleus.physicsObject.AddForce(forceToOrigin);
                }

                float m = 0; // max distance from origin
                nucleus.particles

                // foreach (Particle particle in particles) // iterate through particles
                //{
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
                
                    // foreach (Particle other in particles)
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
                                float overlap = diffOther.magnitude - particle.Radius - other.Radius;

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
                //}
            }
        }
    }
}*/

