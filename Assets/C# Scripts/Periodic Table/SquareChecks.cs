using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareChecks : MonoBehaviour
{
    public GameObject[] squareChecks;
    
    // false: hidden, true: shown
    private bool redSquare = false; 
    private bool orangeSquare = false; 
    private bool yellowSquare = false; 
    private bool greenSquare = false; 
    private bool cyanSquare = false; 
    private bool blueSquare = false; 
    private bool lightPurpleSquare = false; 
    private bool purpleSquare = false;
    private bool graySquare = false;
    private bool tulipanVioletSquare = false;

    private void Start()
    {
        redSquare = false;
        orangeSquare = false; 
        yellowSquare = false; 
        greenSquare = false;
        cyanSquare = false; 
        blueSquare = false; 
        lightPurpleSquare = false; 
        purpleSquare = false;
        graySquare = false;
        tulipanVioletSquare = false;
    }

    private void Update()
    {
        if (redSquare == true && orangeSquare == true && yellowSquare == true && greenSquare == true && cyanSquare == true 
        && blueSquare == true && lightPurpleSquare == true & purpleSquare == true && graySquare == true && tulipanVioletSquare == true)
        {
            HideAllSquareChecks();
        }
    }

    public void RedSquareClicked()
    {
        switch (redSquare)
        {
            case false:
                squareChecks[0].SetActive(true);
                redSquare = true;
                break;
            case true:
                squareChecks[0].SetActive(false);
                redSquare = false;
                break;
        }
    }

    public void OrangeSquareClicked()
    {
        switch (orangeSquare)
        {
            case false:
                squareChecks[1].SetActive(true);
                orangeSquare = true;
                break;
            case true:
                squareChecks[1].SetActive(false);
                orangeSquare = false;
                break;
        }
    }

    public void YellowSquareClicked()
    {
        switch (yellowSquare)
        {
            case false:
                squareChecks[2].SetActive(true);
                yellowSquare = true;
                break;
            case true:
                squareChecks[2].SetActive(false);
                yellowSquare = false;
                break;
        }
    }

    public void GreenSquareClicked()
    {
        switch (greenSquare)
        {
            case false:
                squareChecks[3].SetActive(true);
                greenSquare = true;
                break;
            case true:
                squareChecks[3].SetActive(false);
                greenSquare = false;
                break;
        }
    }

    public void CyanSquareClicked()
    {
        switch (cyanSquare)
        {
            case false:
                squareChecks[4].SetActive(true);
                cyanSquare = true;
                break;
            case true:
                squareChecks[4].SetActive(false);
                cyanSquare = false;
                break;
        }
    }

    public void BlueSquareClicked()
    {
        switch (blueSquare)
        {
            case false:
                squareChecks[5].SetActive(true);
                blueSquare = true;
                break;
            case true:
                squareChecks[5].SetActive(false);
                blueSquare = false;
                break;
        }
    }

    public void LightPurpleSquareClicked()
    {
        switch (lightPurpleSquare)
        {
            case false:
                squareChecks[6].SetActive(true);
                lightPurpleSquare = true;
                break;
            case true:
                squareChecks[6].SetActive(false);
                lightPurpleSquare = false;
                break;
        }
    }

    public void PurpleSquareClicked()
    {
        switch (purpleSquare)
        {
            case false:
                squareChecks[7].SetActive(true);
                purpleSquare = true;
                break;
            case true:
                squareChecks[7].SetActive(false);
                purpleSquare = false;
                break;
        }
    }

    public void GraySquareClicked()
    {
        switch (graySquare)
        {
            case false:
                squareChecks[8].SetActive(true);
                graySquare = true;
                break;
            case true:
                squareChecks[8].SetActive(false);
                graySquare = false;
                break;
        }
    }

    public void TulipanVioletSquareClicked()
    {
        switch (tulipanVioletSquare)
        {
            case false:
                squareChecks[9].SetActive(true);
                tulipanVioletSquare = true;
                break;
            case true:
                squareChecks[9].SetActive(false);
                tulipanVioletSquare = false;
                break;
        }
    }

    public void HideAllSquareChecks()
    {
        redSquare = false;
        orangeSquare = false;
        yellowSquare = false; 
        greenSquare = false;
        cyanSquare = false; 
        blueSquare = false; 
        lightPurpleSquare = false; 
        purpleSquare = false;
        graySquare = false;
        tulipanVioletSquare = false;

        foreach (GameObject check in squareChecks)
        {
            check.SetActive(false);
        }
    }
}
