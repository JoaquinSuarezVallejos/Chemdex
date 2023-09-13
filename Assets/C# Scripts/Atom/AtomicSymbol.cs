using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Atom
{
    public class AtomicSymbol : MonoBehaviour
    {
        [SerializeField] private Atom atom;
        [SerializeField] private TextMeshProUGUI nameUI;
        [SerializeField] private TextMeshProUGUI abbreviationUI;
        [SerializeField] private TextMeshProUGUI atomicNumberUI;
        [SerializeField] private TextMeshProUGUI massNumberUI;
        [SerializeField] private TextMeshProUGUI chargeUI;

        /// Summary: sets the UI for the atomic symbol of an atom (element's name, proton count (atomic number), electron count and mass number.

        private void Update()
        {
            SetSymbol(atom.Element, atom.Nucleus.ProtonCount, atom.Nucleus.Mass, atom.ElectronCount); // set the symbol of the atom
        }

        public void SetSymbol(Element element, int protonCount, int mass, int electronCount) // set the symbol of the atom, including the element, proton count, electron count and mass.
        {
            if (element != null) // if the atom has an element
            {
                nameUI.text = element.Name; // set the name of the element
                abbreviationUI.text = element.Abbreviation; // set the abbreviation of the element
            }

            else // if the atom does not have an element, clear text
            {
                nameUI.text = "";
                abbreviationUI.text = "";
            }

            atomicNumberUI.text = protonCount > 0 ? protonCount.ToString() : ""; // set the atomic number of the atom
            massNumberUI.text = mass > 0 ? mass.ToString() : ""; // set the mass number of the atom

            int charge = protonCount - electronCount; // calculate the charge of the atom

            // set the charge of the atom if necessary (+ or -)
            if (charge > 0)
                chargeUI.text = charge + "+"; 
            else if (charge < 0)
                chargeUI.text = -charge + "-";
            else
                chargeUI.text = "";
        }
    }
}