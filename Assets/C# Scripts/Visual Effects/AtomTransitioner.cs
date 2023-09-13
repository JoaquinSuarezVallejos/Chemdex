using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUI;
using UnityEngine.UI;
using Atom;
using UnityEngine.SceneManagement;

public class AtomTransitioner : MonoBehaviour
{
    [SerializeField] private Trans[] transitions;

    private int index = 0;
    public GameObject atom;
    private Renderer[] children1, children2;

    //Lerp variables
    private const float lerpTime = 1.0f;
    private float currLerpTime = 0.0f;

    public void StartTransition(int index)
    {
        StartCoroutine(StartTransitionWithDelay(index));
    }

    private IEnumerator StartTransitionWithDelay(int index) 
    {
        yield return new WaitForSeconds(0.0025f);

        this.index = index;
        currLerpTime = 0;

        if (transitions[index].atomTransition != null)
        {
            transitions[index].atomTransition.atom.Interactable = transitions[index].atomTransition.interactable;
        }
    }

    private void Update()
    {
        if (currLerpTime < lerpTime)
        {
            currLerpTime += Time.deltaTime;
            if (currLerpTime > lerpTime)
            {
                currLerpTime = lerpTime;
            }
            float p = currLerpTime / lerpTime;
            p = p * p * (3f - 2f * p); //smooth step
            //p = p*p*p*(p*(p*6-15)+10); //smoother step
            //p = Mathf.Pow(p, 1f / 6f);
            foreach (DUITrans DUITrans in transitions[index].DUItransitions)
            {
                DUITrans.Update(p);  
            }
            if (transitions[index].atomTransition != null)
            {
                transitions[index].atomTransition.atom.AdjustScale();
            }
        }
    }

    public void HideMeshRenderer()
    {   
        children1 = atom.GetComponentsInChildren<Renderer>();

        foreach (Renderer child in children1)
        {
            child.enabled = false;
        }
    }

    public void ShowMeshRenderer()
    {   
        children2 = atom.GetComponentsInChildren<Renderer>();

        foreach (Renderer child2 in children2)
        {
            child2.enabled = true;
        }
    }

    /*public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneName:"MainMenu");
    }*/
}

[System.Serializable]
public class Trans
{
    public string name;

    public DUITrans[] DUItransitions;
    //public UITrans[] UItransitions;
    public AtomTrans atomTransition;
}

[System.Serializable]
public class AtomTrans
{
    public Atom.Atom atom;
    public bool interactable;
}

[System.Serializable]
public class DUITrans
{
    public DUIAnchor anchor;
    public Vector2 min;
    public Vector2 max;
    
    public void Update(float p)
    {
        anchor.MinMax = new Vector2[] { Vector2.Lerp(anchor.MinMax[0], min , p),
                                        Vector2.Lerp(anchor.MinMax[1], max, p) };
    }
}

[System.Serializable]
public class UITrans
{
    public RectTransform rectTransform;

    public Vector2 min;
    public Vector2 max;

    public void Update(float p)
    {
        rectTransform.anchorMax = Vector2.Lerp(rectTransform.anchorMax, max, p);
        rectTransform.anchorMin = Vector2.Lerp(rectTransform.anchorMin, min, p);        
    }
}