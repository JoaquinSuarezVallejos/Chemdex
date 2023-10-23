using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorManagerCaller : MonoBehaviour
{
    LevelSelector levelSelector;
    private void Awake()
    {
        levelSelector = GameObject.Find("LevelSelectorManager").GetComponent<LevelSelector>();
    }
    private void Update()
    {
        //levelSelector.OnSceneLoaded();
        Destroy(this);
    }
}
