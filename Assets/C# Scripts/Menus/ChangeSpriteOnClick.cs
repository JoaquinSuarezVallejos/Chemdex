using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteOnClick : MonoBehaviour
{
    [SerializeField] Sprite pressedUI;
    [SerializeField] Sprite notPressedUI;

    public void OnPressed()
    {
        gameObject.GetComponent<Image>().sprite = pressedUI;
    }

    public void OnExitOrMouseUp()
    {
        gameObject.GetComponent<Image>().sprite = notPressedUI;
    }
}
