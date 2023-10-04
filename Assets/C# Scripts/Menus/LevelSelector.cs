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

    public void OnLevelSelectorLoaded()
    {
        level.SetActive(true);
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }
        if (levelAvaible <= lastLevelPassed)
        {
            levelAvaible = lastLevelPassed + 1;
        }
        if (levelAvaible <= levels.Length)
        {
            for (int i = 0; i < levelAvaible; i++)
            {
                Debug.Log("hola");
                levels[i].SetActive(true);
            }
        }
    }

    public void OnOtherSceneLoaded()
    {
        level.SetActive(false);
    }
}
