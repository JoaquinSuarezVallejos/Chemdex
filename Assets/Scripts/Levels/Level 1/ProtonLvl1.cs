﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class ProtonLvl1 : ParticleLvl1
    {
        /// Summary: handles the bahavior of proton particles in atom

        protected override void Awake() // called when the proton is created
        {
            Radius = 0.5f;
            base.Awake();
        }

        protected override void PickUpParticle() // called when the proton is picked up from the atom
        {
            // check if the proton is part of the atom and can be removed
            if (inAtom && atom.Nucleus.RemoveParticle(this))
            {
                base.PickUpParticle(); // pick up the proton (remove it from the atom)
                Debug.Log("Proton Removed");
            }
            else
            {
                atom.RemoveExcessParticle(this);
            }
        }

        protected override void DropParticle() // called when the proton is dropped into the atom
        {
            // check if not already part of the atom, within atom bounds, and can actually be added
            if (!inAtom && (!atom.Interactable || atom.Contains(transform.position) || (atom.Nucleus.ProtonCount == 0 && atom.Nucleus.NeutronCount == 0)) && atom.Nucleus.AddParticle(this))
            {
                base.DropParticle(); // drop the proton (add it to the atom)
                Debug.Log("Proton Added");
            }

            // proton out of bounds or could not be added
            else
            {
                atom.AddExcessParticle(this);
            }
        }
    }
}