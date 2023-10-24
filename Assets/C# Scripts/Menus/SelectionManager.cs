using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public void Levels()
    {
        SceneManager.LoadScene("LevelSelector");
    }
    
    public void AtomConstruction()
    {
        SceneManager.LoadScene("FreePlay");
    }
    
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
