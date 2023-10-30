using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCounterManager : MonoBehaviour
{
    //[SerializeField] int maxProtons;
    //[SerializeField] int maxNeutrons;
    [SerializeField] GameObject[] protonsOnScreen;
    [SerializeField] GameObject[] neutronsOnScreen;
    bool full = false;

    public void newProtonOnScreen(GameObject proton)
    {
        full = true;
        for (int i = 0; i < protonsOnScreen.Length; i++)
        {
            if (protonsOnScreen[i] == null)
            {
                full = false;
                Debug.Log("no lleno");
                break;
            }
        }

        if (!full)
        {
            for (int i = 0; i < protonsOnScreen.Length; i++)
            {
                if (protonsOnScreen[i] == null)
                {
                    protonsOnScreen[i] = proton;
                    break;
                }
            }
        }

        if (full)
        {
            Debug.Log("lleno");
            for (int i = 0; i < protonsOnScreen.Length; i++)
            {
                if (protonsOnScreen[i].transform.parent == null || protonsOnScreen[i].transform.parent.name == "Proton Marker")
                {
                    Destroy(protonsOnScreen[i].gameObject);
                    protonsOnScreen[i] = proton;
                    break;
                }
            }
        }


        //for (int i = 0; i < protonsOnScreen.Length; i++)
        //{
        //    if (protonsOnScreen[i] == null)
        //    {
        //        protonsOnScreen[i] = proton; //When new proton is created, it adds it to the array, if it can.
        //    }
        //    if (i + 1 == protonsOnScreen.Length)
        //    {
        //        Debug.Log("Is full");
        //        full = true;
        //    }
        //    break;
        //}

        //if (full)
        //{
        //    Debug.Log("is filled");
        //}

        //else
        //{
        //    Debug.Log("Not filled");

        //}
    }

    public void newNeutronOnScreen(GameObject neutron)
    {

    }
}
