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
        LevelSelector levelSelector;
        [SerializeField] private int level;

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
            if (levelSelector == null)
            {
                levelSelector = GameObject.Find("LevelSelectorManager").GetComponent<LevelSelector>();
                levelSelector.lastLevelPassed = level;
            }
            else
            {
                levelSelector.lastLevelPassed = level;
            }
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LevelSelector()
        {
            SceneManager.LoadScene("LevelSelector");
        }

        public void NextLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }
    }
}

