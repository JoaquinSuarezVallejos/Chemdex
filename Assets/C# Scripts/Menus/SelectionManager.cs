using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public void Levels()
    {
        SceneManager.LoadScene(2);
    }
    
    public void PeriodicTable()
    {
        SceneManager.LoadScene(3);
    }
    
    public void AtomConstruction()
    {
        SceneManager.LoadScene(4);
    }
    
    public void Settings()
    {
        SceneManager.LoadScene(5);
    }
}
