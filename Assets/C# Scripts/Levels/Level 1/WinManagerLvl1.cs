using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Atom
{
    public class WinManagerLvl1 : MonoBehaviour
    {
        [SerializeField] Levysabe levyScript;
        [SerializeField] GameObject winCanvas;
        [SerializeField] GameObject loseCanvas;
        [SerializeField] GameObject texts;

        private void Awake()
        {
            winCanvas.SetActive(false);
            loseCanvas.SetActive(false);
            texts.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (levyScript.win)
            {
                //canvas.SetActive(true);
                Win();
            }
            else if (levyScript.lose)
            {
                //canvas.SetActive(true);
                Lose();
            }
        }

        private void Lose()
        {
            texts.SetActive(false);
            loseCanvas.SetActive(true);
            //show losing canvas
        }

        private void Win()
        {
            texts.SetActive(false);
            winCanvas.SetActive(true);
            //show winning canvas
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

