using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    public int lastLevelPassed = 0;
    [SerializeField] int levelAvaible = 0;

    // Start is called before the first frame update
    void Awake()
    {
        levels = GameObject.FindGameObjectsWithTag("Levels");
    }

    private void Start()
    {
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
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
