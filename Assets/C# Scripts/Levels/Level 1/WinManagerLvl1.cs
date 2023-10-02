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
                Win();
            }
            else if (levyScript.lose)
            {
                Lose();
            }
        }

        private void Lose()
        {
            texts.SetActive(false);
            loseCanvas.SetActive(true);
        }

        private void Win()
        {
            texts.SetActive(false);
            winCanvas.SetActive(true);
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

