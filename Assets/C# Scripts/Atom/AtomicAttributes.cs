﻿using UnityEngine;
using UnityEngine.UI;

namespace Atom
{
    public class AtomicAttributes : MonoBehaviour
    {
        [SerializeField] private Atom atom; 
        [SerializeField] private Text formalNameUI;
        [SerializeField] private Text stableUI;

        /// Summary: sets the UI for the isotope of an atom, also checks for formal names and stability of the isotope.

        private void Update() 
        {
            if (atom.Element != null) // if the atom has an element
            {
                Isotope isotope = atom.Element.GetIsotope(atom.Nucleus.Mass); // get the isotope of the atom

                if (isotope != null) // if the atom is an isotope
                {
                    formalNameUI.text = isotope.FormalName.Length > 0 ? isotope.FormalName // if the isotope has a formal name, use it
                                                                      : atom.Element.Name + "-" + atom.Nucleus.Mass; // if the isotope does not have a formal name, use the element name and mass
                    stableUI.text = isotope.Stable ? "Stable" : "Radioactive"; // if the isotope is stable, say it is stable, otherwise say it is radioactive
                }

                else // if the atom is not an isotope, it is radioactive
                {
                    formalNameUI.text = "Not Isotope";
                    stableUI.text = "Radioactive";
                }
            }

            else  // the atom is empty, clear text
            {
                formalNameUI.text = "";
                stableUI.text = "";
            }
        }
    }
}