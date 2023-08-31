using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBlankElementIcon : MonoBehaviour
{
    public GameObject elementIcon, blankElementIcon;

    public void SetBlankIcon()
    {
        elementIcon.SetActive(false);
        blankElementIcon.SetActive(true);
    }
}
