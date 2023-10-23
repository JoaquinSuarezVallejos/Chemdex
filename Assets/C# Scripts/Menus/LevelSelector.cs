using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject[] levels = new GameObject[1];
    public int lastLevelPassed = 0;
    [SerializeField] int levelAvaible = 0;
    public static LevelSelector instance;
    GameObject level;
    string sceneName;
    //[SerializeField] Sprite clickableUI;
    //[SerializeField] Sprite clickedUI;
    //[SerializeField] Sprite lockedUI;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        level = gameObject.transform.GetChild(0).gameObject;
        levels = GameObject.FindGameObjectsWithTag("Levels");
    }
    void Update()
    {
        if (sceneName != SceneManager.GetActiveScene().name)
        {
            OnSceneLoaded();
            sceneName = SceneManager.GetActiveScene().name;
        }
    }

    public void OnLevelSelected(int levelIndex)
    {
        SceneManager.LoadScene("Level " + levelIndex);
    }

    public void OnSceneLoaded()
    {
        if (SceneManager.GetActiveScene().name == "LevelSelector")
        {
            OnLevelSelectorLoaded();
        }

        else
        {
            OnOtherSceneLoaded();
        }
    }

    public void OnLevelSelectorLoaded(Sprite notPassedClickableUI = default(Sprite))
    {
        level.SetActive(true);
        foreach (GameObject level in levels)
        {
            level.SetActive(true);
            level.GetComponent<Button>().interactable = false; //No interactable, set image to "locked" level.
        }
        if (levelAvaible <= lastLevelPassed)
        {
            levelAvaible = lastLevelPassed + 1;
        }
        if (levelAvaible <= levels.Length)
        {
            for (int i = 0; i < levelAvaible; i++)
            {
                levels[i].GetComponent<Image>().sprite = notPassedClickableUI;
                levels[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void OnPressed(GameObject objet, Sprite clickedUI)
    {
        if (objet.GetComponent<Button>().interactable)
        {
            objet.GetComponent<Image>().sprite = clickedUI;
        }
    }

    public void OnNotPressed(GameObject objet, Sprite clickableUI, Sprite notClickableUI)
    {
        if (objet.GetComponent<Button>().interactable)
        {
            objet.GetComponent<Image>().sprite = clickableUI;
        }
        else
        {
            objet.GetComponent<Image>().sprite = notClickableUI;
        }
    }

    public void OnOtherSceneLoaded()
    {
        level.SetActive(false);
    }
}
