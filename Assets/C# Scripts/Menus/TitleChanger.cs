using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelMolecule;
    private string molecule;
    [SerializeField] float counter;

    // Start is called before the first frame update
    void Start()
    {
        levelMolecule.text = "SELECT A LEVEL";
    }

    public void ChangeTitleWhileOver(string _molecule)
    {
        molecule = _molecule;
        StartCoroutine(MouseOverLevelButton());
    }

    public void ChangeTitleWhenOut()
    {
        StopAllCoroutines();
        levelMolecule.text = "SELECT A LEVEL";
    }

    IEnumerator MouseOverLevelButton()
    {
        yield return new WaitForSeconds(counter);
        if (GetComponent<Button>().interactable)
        {
            levelMolecule.text = molecule;
        }

        else
        {
            levelMolecule.text = "???";
        }
    }
}
