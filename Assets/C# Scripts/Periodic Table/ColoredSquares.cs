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

    private ColoredSquareManager managerScript;

    GameObject[] reset;

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

        //reset = periodicTable. 

        managerScript = GameObject.Find("Square Color Manager").GetComponent<ColoredSquareManager>();
    }

    private void OnMouseDown()
    {
        managerScript.OnClicked(ReturnColorType());
    }

    GameObject[] ReturnColorType()
    {
        switch (gameObject.name)
        {
            case "RedShapeTable": //Alkali_metals
                return Alkali_metals;

            case "OrangeShapeTable": // Alkaline_earth_metals
                return Alkaline_earth_metals;

            case "YellowShapeTable": // Transition_metals
                return Transition_metals;

            case "GreenShapeTable": // Post_Transition_metals
                return Post_Transition_metals;

            case "CyanShapeTable": // Metalloids
                return Metalloids;

            case "BlueShapeTable": // Nonmetals
                return Nonmetals;

            case "LightPurpleShapeTable": // Halogens
                return Halogens;

            case "PurpleShapeTable": // Noble_gases
                return Noble_gases;

            case "GrayShapeTable": // Lanthanides
                return Lanthanides;

            case "TulipanVioletShapeTable": // Actinides
                return Actinides;

            case "Reset":
                return null;
        }
        return null;
    }
}