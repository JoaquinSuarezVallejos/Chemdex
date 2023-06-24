using UnityEngine;
using UnityEngine.UI;

namespace Atom
{
    public class AtomicAttributes : MonoBehaviour
    {
        [SerializeField] private Atom atom;
        [SerializeField] private Text formalNameUI;
        [SerializeField] private Text stableUI;

        private void Update()
        {
            if (atom.Element != null)
            {
                Isotope isotope = atom.Element.GetIsotope(atom.Nucleus.Mass);

                if (isotope != null)
                {
                    formalNameUI.text = isotope.FormalName.Length > 0 ? isotope.FormalName 
                                                                      : atom.Element.Name + "-" + atom.Nucleus.Mass;
                    stableUI.text = isotope.Stable ? "Stable" : "Radioactive";
                }
                else
                {
                    formalNameUI.text = "Not Isotope";
                    stableUI.text = "Radioactive";
                }
            }
            else
            {
                formalNameUI.text = "";
                stableUI.text = "";
            }
        }
    }
}

