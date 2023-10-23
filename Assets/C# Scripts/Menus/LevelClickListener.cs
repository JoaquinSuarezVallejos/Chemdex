using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelClickListener : MonoBehaviour
{
    LevelSelector script;
    [SerializeField] GameObject scriptObject;
    [SerializeField] Sprite PassedClickeableUI;
    [SerializeField] Sprite PassedClickedUI;
    [SerializeField] Sprite lockedUI;
    [SerializeField] Sprite NotPassedClickeableUI;
    [SerializeField] Sprite NotPassedAndPressedUI;

    private void Awake()
    {
        script = scriptObject.GetComponent<LevelSelector>();
        script.OnLevelSelectorLoaded(NotPassedClickeableUI);
    }

    public void OnMouseOverAndPressed()
    {
        script.OnPressed(gameObject, PassedClickedUI);
    }

    public void OnMouseLeftOrUP()
    {
        script.OnNotPressed(gameObject, PassedClickeableUI, lockedUI);
    }
}
