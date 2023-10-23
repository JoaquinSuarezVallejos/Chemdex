using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelClickListener : MonoBehaviour
{
    LevelSelector script;
    [SerializeField] GameObject scriptObject;
    public Sprite PassedClickeableUI;
    public Sprite PassedClickedUI;
    public Sprite lockedUI;
    public Sprite NotPassedClickeableUI;
    public Sprite NotPassedAndPressedUI;
    private string sceneName;
    public bool levelPassed;

    private void Awake()
    {
        script = scriptObject.GetComponent<LevelSelector>();
    }

    void Update()
    {
        if (sceneName != SceneManager.GetActiveScene().name)
        {
            OnSceneLoaded();
            sceneName = SceneManager.GetActiveScene().name;
        }
    }

    public void OnSceneLoaded()
    {
        if (SceneManager.GetActiveScene().name == "LevelSelector")
        {
            script.OnLevelSelectorLoaded(gameObject, NotPassedClickeableUI, PassedClickeableUI);
        }

        else
        {
            script.OnOtherSceneLoaded();
            script.onLevelSelector = false;
        }
    }

    public void OnMouseOverAndPressed()
    {
        script.OnPressed(gameObject, NotPassedAndPressedUI, PassedClickedUI, lockedUI);
    }

    public void OnMouseLeftOrUP()
    {
        script.OnNotPressed(gameObject, NotPassedClickeableUI, PassedClickeableUI ,lockedUI);
    }
}
