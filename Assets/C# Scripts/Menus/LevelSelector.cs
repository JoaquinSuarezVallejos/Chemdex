using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    public int lastLevelPassed = 0;
    [SerializeField] int levelAvaible = 0;
    public static LevelSelector instance;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LevelSelector")
        {
            if (levelAvaible <= lastLevelPassed)
            {
                levelAvaible = lastLevelPassed + 1;
            }
            if (levelAvaible <= levels.Length)
            {
                for (int i = 0; i < levelAvaible; i++)
                {
                    levels[i].SetActive(true);
                }
            }
        }
    }

    public void OnLevelSelected(int levelIndex)
    {
        SceneManager.LoadScene("Level " + levelIndex);
    }

    public void OnSceneLoaded()
    {
        levels = GameObject.FindGameObjectsWithTag("Levels");
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }
    }
}
