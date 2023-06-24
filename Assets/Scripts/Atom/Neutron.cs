using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Neutron : Particle
    {
        /// <summary>
        /// Handles the bahavior of neutron particles in atom
        /// </summary>

        protected override void Awake()
        {
            Radius = 0.5f;

            base.Awake();
        }

        protected override void PickUpParticle()
        {
            //check the neutron is part of the atom and can be removed
            if (inAtom && atom.Nucleus.RemoveParticle(this))
            {
                base.PickUpParticle();
                Debug.Log("Neutron Removed");
            }
            else
            {
                atom.RemoveExcessParticle(this);
            }
        }

        protected override void DropParticle()
        {
            //check not already part of atom, within atom bounds, and can actually be added
            if (!inAtom && (!atom.Interactable || atom.Contains(transform.position) || atom.Nucleus.Mass < atom.Nucleus.MassMin) && atom.Nucleus.AddParticle(this))
            {
                base.DropParticle();
                Debug.Log("Neutron Added");
            }
            //neutron out of bounds or could not be added
            else
            {
                atom.AddExcessParticle(this);
            }
        }
    }

    
}

    
