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
    public bool onLevelSelector;
    bool modificados = false;

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
    }
    
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "LevelSelector")
        {
            level.SetActive(true);
        }

        if (lastLevelPassed == 15 && !modificados)
        {
            modificados = true;
            foreach (GameObject level in levels)
            {
                level.GetComponent<Button>().interactable = true;
                level.GetComponent<LevelClickListener>().levelPassed = true;
                level.GetComponent<Image>().sprite = level.GetComponent<LevelClickListener>().PassedClickeableUI;
            }
        }

        if (onLevelSelector && !modificados)
        {
            foreach (GameObject level in levels)
            {
                if (level.GetComponent<Button>().interactable)
                {
                    level.GetComponent<Image>().sprite = level.GetComponent<LevelClickListener>().NotPassedClickeableUI;
                    Debug.Log("paso a gris clickeable");
                }

                if (level.GetComponent<LevelClickListener>().levelPassed)
                {
                    level.GetComponent<Image>().sprite = level.GetComponent<LevelClickListener>().PassedClickeableUI;
                    Debug.Log("paso a verde clickeable");
                }
            }
            modificados = true;
        }
    }

    public void OnLevelSelected(int levelIndex)
    {
        SceneManager.LoadScene("Level " + levelIndex);
    }

    public void OnLevelSelectorLoaded(GameObject objet, Sprite notPassedClickableUI, Sprite passedClickableUI)
    {
        onLevelSelector = true;
        modificados = false;
        foreach (GameObject level in levels)
        {
            level.GetComponent<Button>().interactable = false;
        }
        if (levelAvaible <= lastLevelPassed)
        {
            levelAvaible = lastLevelPassed + 1;
        }
        if (levelAvaible <= levels.Length)
        {
            for (int i = 0; i < levelAvaible; i++) //Todos los niveles disponibles pueden ser clickeables.
            {
                levels[i].GetComponent<Button>().interactable = true;
            }

            for (int i = 0; i < lastLevelPassed; i++) //Todos los niveles disponibles pueden ser clickeables.
            {
                levels[i].GetComponent<LevelClickListener>().levelPassed = true;
            }
        }
    }

    public void OnPressed(GameObject objet, Sprite notPassedClickedUI, Sprite passedClickedUI, Sprite lockedUI)
    {
        if (objet.GetComponent<Button>().interactable)
        {
            if (objet.GetComponent<LevelClickListener>().levelPassed)
            {
                objet.GetComponent<Image>().sprite = passedClickedUI; //lo cambio a verde clickeado.
            }
            else
            {
                objet.GetComponent<Image>().sprite = notPassedClickedUI; //lo cambio a gris clickeado.
            }
        }
        else
        {
            objet.GetComponent<Image>().sprite = lockedUI; //si no es interactuable, lo paso a bloqueado.
        }
    }

    public void OnNotPressed(GameObject objet, Sprite notPassedClickeableUI, Sprite passedClickeableUI, Sprite lockedUI)
    {
        if (objet.GetComponent<Button>().interactable)
        {
            if (objet.GetComponent<LevelClickListener>().levelPassed)
            {
                objet.GetComponent<Image>().sprite = passedClickeableUI;
                Debug.Log("paso a verde clickeable");
            }
            if (!objet.GetComponent<LevelClickListener>().levelPassed)
            {
                objet.GetComponent<Image>().sprite = notPassedClickeableUI;
                Debug.Log("paso a gris clickeable");
            }
        }
        else
        {
            objet.GetComponent<Image>().sprite = lockedUI;
        }
    }

    public void OnOtherSceneLoaded()
    {
        level.SetActive(false);
    }
}
