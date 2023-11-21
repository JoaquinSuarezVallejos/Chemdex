using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Physics;

namespace Atom
{
    public class WinManagerLvl1 : MonoBehaviour
    {

        #region UI
        [SerializeField] GameObject winCanvas;
        [SerializeField] GameObject loseCanvas;
        [SerializeField] GameObject texts;
        #endregion
        #region Scripts
        [SerializeField] Levysabe levyScript;
        LevelSelector levelSelector;
        SettingsManager gameSettings;
        #endregion
        #region Audio
        [SerializeField] AudioSource source;
        [SerializeField] AudioClip winClip;
        [SerializeField] AudioClip loseClip;
        bool audioPlayed = false;
        #endregion

        [SerializeField] private int level;
        
        public float counter = 1;
        
        [SerializeField] GameObject[] confetti;

        Animator anim;

        public bool shaking = false;

        private void Awake()
        {
            winCanvas.SetActive(false);
            loseCanvas.SetActive(false);
            texts.SetActive(true);
            anim = winCanvas.GetComponent<Animator>();
            source = GetComponent<AudioSource>();
            audioPlayed = false;
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
            /*if (GameObject.Find("Settings Manager") != null)
            {
                gameSettings = GameObject.Find("Settings Manager").GetComponent<SettingsManager>();
                if (!audioPlayed && gameSettings.soundEffects)
                {
                    source.PlayOneShot(loseClip);
                    audioPlayed = true;
                }
            }*/

            if (!audioPlayed)
            {
                audioPlayed = true;
                AudioManager.Instance.PlaySFX("LoseEffectShake");
            }
            TryAgain();
        }

        private void Win()
        {
            if (!audioPlayed)
            {
                //source.PlayOneShot(winClip);
                audioPlayed = true;
                AudioManager.Instance.PlaySFX("WinEffectConfetti");
            }

            StartCoroutine(wait1SecondAfterWin());
            foreach (GameObject confeti in confetti)
            {
                confeti.SetActive(true);
            }
            StartCoroutine(levyScript.waitToMoveMolecule(levyScript.counter));
            levyScript.win = false;
        }

        public void TryAgain()
        {
            shaking = true;
            StartCoroutine(waitHalfSecondAfterLosing());
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

        IEnumerator waitHalfSecondAfterLosing()
        {
            yield return new WaitForSeconds(0.5f);
            shaking = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

