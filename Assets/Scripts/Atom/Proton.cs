using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Proton : Particle
    {
        /// <summary>
        /// Handles the bahavior of proton particles in atom
        /// </summary>
        
        protected override void Awake()
        {
            Radius = 0.5f;

            base.Awake();
        }

        protected override void PickUpParticle()
        {
            //check the proton is part of the atom and can be removed
            if (inAtom && atom.Nucleus.RemoveParticle(this))
            {
                base.PickUpParticle();
                Debug.Log("Proton Removed");
            }
            else
            {
                atom.RemoveExcessParticle(this);
            }
        }

        protected override void DropParticle()
        {
            if (!inAtom && (!atom.Interactable || atom.Contains(transform.position) || atom.Nucleus.ProtonCount == 0) && atom.Nucleus.AddParticle(this))
            {
                base.DropParticle();
                Debug.Log("Proton Added");
            }
            //proton out of bounds or could not be added
            else
            {
                atom.AddExcessParticle(this);
            }
        }
    }
}
