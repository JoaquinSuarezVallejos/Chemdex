using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAllElementIcons : MonoBehaviour
{
    public GameObject[] allElementIcons;

    void Update()
    {
        allElementIcons = GameObject.FindGameObjectsWithTag("ElementBigIcon");
    }

    public void IfClickedOn()
    {
        foreach (GameObject g in allElementIcons)
        {
            g.SetActive(false);
        }

        Debug.Log("Clicked");
    }
}
