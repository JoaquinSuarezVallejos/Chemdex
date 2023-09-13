using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class WinManagerLvl1 : MonoBehaviour
    {
        [SerializeField] Levysabe levyScript;
        [SerializeField] GameObject canvas;

        // Update is called once per frame
        void Update()
        {
            if (levyScript.win)
            {
                canvas.SetActive(true);
            }
        }
    }
}

