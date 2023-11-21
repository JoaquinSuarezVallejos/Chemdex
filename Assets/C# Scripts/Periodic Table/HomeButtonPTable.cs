using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonPTable : MonoBehaviour
{
    public void HomeButtonPressed()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
