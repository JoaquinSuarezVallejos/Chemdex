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
    [SerializeField] int counter;
    [SerializeField] GameObject[] squareChecks;
    private ColoredSquares coloredSquaresScript;
    private SquareChecks squareChecksScript;


    private void Awake()
    {
        allImagesOfElements = periodicTable.GetComponentsInChildren<Image>();
    }

    private void Start()
    {
        coloredSquaresScript = GameObject.Find("RedShapeTable").GetComponent<ColoredSquares>();
        squareChecksScript = GameObject.Find("SquareChecks").GetComponent<SquareChecks>();
        //squareChecks = GameObject.FindGameObjectsWithTag("SquareCheck");
    }

    public void OnClicked(GameObject[] hidingList)
    {
        if (!firstMouseDownClick && hidingList != null)
        {
            HideAllImages();
            ShowAllImagesOfThisElement(hidingList);
            firstMouseDownClick = true;
            counter += 1;
            return;
        }

        if (hidingList != null)
        {
            Debug.Log("no nula");
            if (hidingList[0].GetComponent<Image>().color.a == 1) //esta mostrado
            {
                Debug.Log("oculta lo mostrado");
                HideAllImagesOfThisElement(hidingList);
                HideSpecificCheck();
                counter -= 1;
            }
            else if (hidingList[0].GetComponent<Image>().color.a == 0.3f) //esta oculto
            {
                Debug.Log("muestra lo oculto");
                ShowAllImagesOfThisElement(hidingList);
                Debug.Log(hidingList);
                ShowSpecificCheck();
                counter += 1;
            }

            if (counter == 10)
            {
                Debug.Log("vuelve a la normalidad");
                HideAllSquareChecks();
                Debug.Log("checks hided");
                ShowAllImages();

                firstMouseDownClick = false;
                counter = 0;
            }

            if (counter == 0)
            {
                Debug.Log("vuelve a la normalidad");
                HideAllSquareChecks();
                Debug.Log("checks hided");
                ShowAllImages();

                firstMouseDownClick = false;
                counter = 0;
            }
        }

        if (hidingList == null)
        {
            ShowAllImages();
            Debug.Log("reseteo");
            firstMouseDownClick = false;
            counter = 0;
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

    private void HideAllImagesOfThisElement(GameObject[] ElementList)
    {
        foreach (GameObject element in ElementList)
        {
            image = element.GetComponent<Image>();
            Color tempColor = image.color;
            tempColor.a = 0.3f;
            image.color = tempColor;
        }
    }

    private void ShowAllImages()
    {
        for (int i = 0; i < allImagesOfElements.Length; i++)
        {
            image = allImagesOfElements[i].GetComponent<Image>();
            Color tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
            UIArrows.GetComponent<Image>().enabled = true;
        }
    }

    private void HideAllSquareChecks()
    {
        foreach (GameObject check in squareChecks)
        {
            check.SetActive(false);
        }
    }

    private void ShowSpecificCheck()
    {
        switch (coloredSquaresScript.squareCheckName)
        {
            case "RedCheck":
                squareChecksScript.squareChecks[0].SetActive(true);
                return;
        }
    }

    private void HideSpecificCheck()
    {
        switch (coloredSquaresScript.squareCheckName)
        {
            case "RedCheck":
                squareChecksScript.squareChecks[0].SetActive(false);
                return;
        }
    }
}
