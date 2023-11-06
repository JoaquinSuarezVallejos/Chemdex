using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoredSquareManager : MonoBehaviour
{
    bool firstMouseDownClick = false;
    private Image image;
    private Image[] allImagesOfElements;
    [SerializeField] GameObject periodicTable, UIArrows;

    private void Awake()
    {
        allImagesOfElements = periodicTable.GetComponentsInChildren<Image>();
    }

    public void OnClicked(GameObject[] hidingList)
    {
        if (!firstMouseDownClick)
        {
            HideAllImages();
            ShowAllImagesOfThisElement(hidingList);
            firstMouseDownClick = true;
        }
    }

    private void ShowAllImagesOfThisElement(GameObject[] ElementList)
    {
        foreach (GameObject element in ElementList)
        {
            image = element.GetComponent<Image>();
            Color tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }
    }

    private void HideAllImages()
    {
        for (int i = 0; i < allImagesOfElements.Length; i++)
        {
            image = allImagesOfElements[i].GetComponent<Image>();
            Color tempColor = image.color;
            tempColor.a = 0.3f;
            image.color = tempColor;
            UIArrows.GetComponent<Image>().enabled = false;
        }
    }
}
