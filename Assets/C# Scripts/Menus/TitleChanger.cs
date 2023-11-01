using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelMolecule;
    //LevelClickListener isLevelPassedScript;

    // Start is called before the first frame update
    void Start()
    {
        levelMolecule.text = "SELECT A LEVEL";
        //isLevelPassedScript = GetComponent<LevelClickListener>();
    }

    public void ChangeTitleWhileOver(string molecule)
    {
        if (GetComponent<Button>().interactable)
        {
            levelMolecule.text = molecule;
        }

        else
        {
            levelMolecule.text = "???";
        }
    }

    public void ChangeTitleWhenOut()
    {
        levelMolecule.text = "SELECT A LEVEL";
    }
}
