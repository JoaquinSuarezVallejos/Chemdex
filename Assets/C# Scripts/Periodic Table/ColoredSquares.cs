using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoredSquares : MonoBehaviour
{
    [SerializeField] GameObject[] Alkali_metals, Alkaline_earth_metals, Transition_metals, 
        Post_Transition_metals, Metalloids, Nonmetals, 
        Halogens, Noble_gases, Lanthanides, Actinides;

    [SerializeField] GameObject[] allElements;

    private Image image;

    private void Update()
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
    }

    private void OnMouseOver()
    {
        switch (gameObject.name)
        {
            case "RedShapeTable": //Alkali_metals
                foreach (GameObject g in Alkali_metals)
                {
                    image = g.GetComponent<Image>();
                    Color tempColor = image.color;
                    tempColor.a = 0.3f;
                    image.color = tempColor;
                }
                break;

            case "OrangeShapeTable":
                Debug.Log("OrangeShape");
                break;

        }
        
    }

}
