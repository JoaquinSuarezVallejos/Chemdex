using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Atom
{
    public class StarterHydrogen : MonoBehaviour
    {
        [SerializeField] private Atom atom;
        [SerializeField] private GameObject introductionMenuCanvas;
        private TextMeshProUGUI[] text;
        public Element Element { get; private set; }

        private void Start()
        {
            introductionMenuCanvas.SetActive(true);
        }

        public void IfClickedOn()
        {
            introductionMenuCanvas.SetActive(false);
        }

        private void Awake()
        {
            text = GetComponentsInChildren<TextMeshProUGUI>();

            int protonCount = 1;

            Element element = Elements.GetElement(protonCount);
            if (element != null)
            {
                //Hook up button to show the element data
                Button b = text[0].GetComponentInParent<Button>();

                if (b != null)
                {
                    b.onClick.AddListener(() => SetElement(protonCount));
                }
            }
        }

        private void SetElement(int protonCount)
        {
            Debug.Log("Show element: " + Elements.GetElement(protonCount).Name);

            if (atom != null)
            {
                atom.ForceToCommon(protonCount);
            }
        }
    }
}
