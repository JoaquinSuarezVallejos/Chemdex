using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowElementIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject elementIcon, blankElementIcon;

    void Start()
    {
        elementIcon.SetActive(false);
        blankElementIcon.SetActive(true);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        elementIcon.SetActive(true);
        blankElementIcon.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        elementIcon.SetActive(false);
        blankElementIcon.SetActive(true);
    }
}
