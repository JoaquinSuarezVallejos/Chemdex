using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Electron : Particle
    {
        /// Summary: handles the behavior of electron particles in the atom.

        public new float Radius
        {
            get { return transform.localScale.x / 2; } // returns the radius of the electron

            set
            { 
                transform.localScale = Vector3.one * 2.0f * value; // sets the radius of the electron
                GetComponent<TrailRenderer>().startWidth = value; // sets the width of the trail of the electron
            }
        }

        protected override void Awake() // called when the electron is created
        {
            base.Awake();
            Radius = 0.25f;
        }

        protected override void PickUpParticle() // called when the electron is picked up from the atom
        {
            // check whether the electron is part of the atom and can be removed
            if (inAtom && atom.RemoveElectron(this)) 
            {
                base.PickUpParticle(); // pick up the electron (remove it from the atom)
                Debug.Log("Electron Removed");
            }

            else
            {
                atom.RemoveExcessParticle(this);
            }
        }

        protected override void DropParticle() // called when the electron is dropped into the atom
        {
            // electron must be added when Next Shell exists and isn't Full
            bool mustAdd = atom.OuterShell != null && atom.OuterShell.NextShell != null && !atom.OuterShell.NextShell.Full; 
            
            // check if not already part of the atom, within atom bounds, and if it can actually be added
            if (!inAtom && (!atom.Interactable || atom.Contains(transform.position) || mustAdd ) && atom.OuterShell.AddParticle(this))
            {
                base.DropParticle(); // drop the electron (add it to the atom)
                Debug.Log("Electron Added");
            }

            // electron out of bounds or cound not be added 
            else
            {
                atom.AddExcessParticle(this);
            }
        }
    }
}