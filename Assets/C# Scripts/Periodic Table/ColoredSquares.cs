using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoredSquares : MonoBehaviour
{
    [SerializeField] GameObject[] Alkali_metals, Alkaline_earth_metals, Transition_metals, 
        Post_Transition_metals, Metalloids, Nonmetals, 
        Halogens, Noble_gases, Lanthanides, Actinides;

    [SerializeField] GameObject periodicTable, UIArrows;

    private Image image;
    private Image[] allImagesOfElements;
    private bool firstMouseDownClick = false;

    private ColoredSquareManager managerScript;

    private void Start()
    {
        Alkali_metals = GameObject.FindGameObjectsWithTag("Alkali_metals");
        Alkaline_earth_metals = GameObject.FindGameObjectsWithTag("Alkaline_earth_metals");
        Transition_metals = GameObject.FindGameObjectsWithTag("Transition_metals");
        Post_Transition_metals = GameObject.FindGameObjectsWithTag("Post_Transition_metals");
        Metalloids = GameObject.FindGameObjectsWithTag("Metalloids");
        Nonmetals = GameObject.FindGameObjectsWithTag("Nonmetals");
        Halogens = GameObject.FindGameObjectsWithTag("Halogens");
        Noble_gases = GameObject.FindGameObjectsWithTag("Noble_gases");
        Lanthanides = GameObject.FindGameObjectsWithTag("Lanthanides");
        Actinides = GameObject.FindGameObjectsWithTag("Actinides");

        allImagesOfElements = periodicTable.GetComponentsInChildren<Image>();

        managerScript = GameObject.Find("Square Color Manager").GetComponent<ColoredSquareManager>();
    }

    private void OnMouseDown()
    {
        managerScript.OnClicked(ReturnColorType());
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

    GameObject[] ReturnColorType()
    {
        switch (gameObject.name)
        {
            case "RedShapeTable": //Alkali_metals
                ShowAllImagesOfThisElement(Alkali_metals);
                return Alkali_metals;

            case "OrangeShapeTable": // Alkaline_earth_metals
                ShowAllImagesOfThisElement(Alkaline_earth_metals);
                return Alkaline_earth_metals;

            case "YellowShapeTable": // Transition_metals
                ShowAllImagesOfThisElement(Transition_metals);
                return Transition_metals;

            case "GreenShapeTable": // Post_Transition_metals
                ShowAllImagesOfThisElement(Post_Transition_metals);
                return Post_Transition_metals;

            case "CyanShapeTable": // Metalloids
                ShowAllImagesOfThisElement(Metalloids);
                return Metalloids;

            case "BlueShapeTable": // Nonmetals
                ShowAllImagesOfThisElement(Nonmetals);
                return Nonmetals;

            case "LightPurpleShapeTable": // Halogens
                ShowAllImagesOfThisElement(Halogens);
                return Halogens;

            case "PurpleShapeTable": // Noble_gases
                ShowAllImagesOfThisElement(Noble_gases);
                return Noble_gases;

            case "GrayShapeTable": // Lanthanides
                ShowAllImagesOfThisElement(Lanthanides);
                return Lanthanides;

            case "TulipanVioletShapeTable": // Actinides
                ShowAllImagesOfThisElement(Actinides);
                return Actinides;
        }
        return null;
    }
}