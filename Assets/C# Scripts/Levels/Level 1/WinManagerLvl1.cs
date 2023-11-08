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
        public float counter = 1;
        [SerializeField] GameObject[] confetti;

        Animator anim;

        private void Awake()
        {
            winCanvas.SetActive(false);
            loseCanvas.SetActive(false);
            texts.SetActive(true);
            anim = winCanvas.GetComponent<Animator>();
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
            foreach (GameObject confeti in confetti)
            {
                confeti.SetActive(true);
                //play victory sound;
            }
            StartCoroutine(wait1SecondAfterWin());
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

        IEnumerator wait1SecondAfterWin()
        {
            yield return new WaitForSeconds(counter);
            texts.SetActive(false);
            winCanvas.SetActive(true);
            anim.SetBool("Win", true);
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
    }
}

