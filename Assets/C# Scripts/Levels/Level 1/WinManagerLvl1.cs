using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class WinManagerLvl1 : MonoBehaviour
    {
        [SerializeField] Levysabe levyScript;
        [SerializeField] GameObject canvas;

        // Start is called before the first frame update
        void Start()
        {
            
        }

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

