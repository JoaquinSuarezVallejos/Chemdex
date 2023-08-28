using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DUI
{
    [RequireComponent(typeof(DUIAnchor))]
    public class DUILayout : MonoBehaviour
    {
        /// <summary>
        /// Calculates anchor min max values for children to layout Anchors in row
        /// </summary>

        public enum DUILayoutType { Horizontal, Vertical /*, Grid */ }

        private DUIAnchor anchor; //ref to attached DUIAnchor

        [SerializeField] private DUILayoutType layout;

        private void Awake()
        {
            //parent node starts the recursive call using the full camera space as bounds
            if (transform.parent.GetComponent<DUILayout>() == null)
            {
                SetAnchors();
            }
        }

        public void SetAnchors()
        {
            anchor = GetComponent<DUIAnchor>();

            List<DUIAnchor> duias = new List<DUIAnchor>();
            for (int i = 0; i < transform.childCount; i++)
            {
                duias.Add(transform.GetChild(i).GetComponent<DUIAnchor>());
            }


            //calculate the offset between anchors
            Vector2 offset = new Vector2(layout == DUILayoutType.Horizontal ? 1.0f / (duias.Count) : 0,
                                         layout == DUILayoutType.Vertical ? 1.0f / (duias.Count) : 0);

            //set the min and max of every anchor based on anchor
            for (int i = 0; i < duias.Count; i++)
            {
                switch (layout)
                {
                    case DUILayoutType.Vertical:
                        duias[i].SetMinMax(Vector2.up - (i * offset), Vector2.one - ((i + 1) * offset));
                        break;
                    case DUILayoutType.Horizontal:
                        duias[i].SetMinMax(i * offset, ((i + 1) * offset) + Vector2.up);
                        break;
                }

                DUILayout duil = duias[i].GetComponent<DUILayout>();
                if (duil != null)
                    duil.SetAnchors();
            }
        }
    }
}
