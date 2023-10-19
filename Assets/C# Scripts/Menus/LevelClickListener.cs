using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelClickListener : MonoBehaviour
{
    LevelSelector script;
    [SerializeField] GameObject scriptObject;

    private void Awake()
    {
        script = scriptObject.GetComponent<LevelSelector>();
    }

    public void OnMouseOverAndPressed()
    {
        script.OnPressed(gameObject);
    }

    public void OnMouseLeftOrUP()
    {
        script.OnNotPressed(gameObject);
    }
}
