using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCounterManager : MonoBehaviour
{

    [SerializeField] GameObject[] protonsOnScreen;
    [SerializeField] GameObject[] neutronsOnScreen;
    [SerializeField] GameObject[] thirdParticleOnScreen;
    bool full = false;
    bool destroyed = false;

    public void newProtonOnScreen(GameObject proton)
    {
        full = true;
        for (int i = 0; i < protonsOnScreen.Length; i++)
        {
            if (protonsOnScreen[i] == null)
            {
                full = false;
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
            for (int i = 0; i < protonsOnScreen.Length; i++)
            {
                if (protonsOnScreen[i].transform.parent == null || protonsOnScreen[i].transform.parent.name == "First Marker")
                {
                    Destroy(protonsOnScreen[i].gameObject);
                    protonsOnScreen[i] = proton;
                    destroyed = true;
                    break;
                }
            }
            if (!destroyed)
            {
                Destroy(proton);
            }
            destroyed = false;
        }
    }

    public void newNeutronOnScreen(GameObject neutron)
    {
        full = true;
        for (int i = 0; i < neutronsOnScreen.Length; i++)
        {
            if (neutronsOnScreen[i] == null)
            {
                full = false;
                break;
            }
        }

        if (!full)
        {
            for (int i = 0; i < neutronsOnScreen.Length; i++)
            {
                if (neutronsOnScreen[i] == null)
                {
                    neutronsOnScreen[i] = neutron;
                    break;
                }
            }
        }

        if (full)
        {
            for (int i = 0; i < neutronsOnScreen.Length; i++)
            {
                if (neutronsOnScreen[i].transform.parent == null || neutronsOnScreen[i].transform.parent.name == "Second Marker")
                {
                    Destroy(neutronsOnScreen[i].gameObject);
                    neutronsOnScreen[i] = neutron;
                    destroyed = true;
                    break;
                }
            }
            if (!destroyed)
            {
                Destroy(neutron);
            }
            destroyed = false;
        }
    }

    public void newThirdParticleOnScreen(GameObject particle)
    {
        full = true;
        for (int i = 0; i < thirdParticleOnScreen.Length; i++)
        {
            if (thirdParticleOnScreen[i] == null)
            {
                full = false;
                break;
            }
        }

        if (!full)
        {
            for (int i = 0; i < thirdParticleOnScreen.Length; i++)
            {
                if (thirdParticleOnScreen[i] == null)
                {
                    thirdParticleOnScreen[i] = particle;
                    break;
                }
            }
        }

        if (full)
        {
            for (int i = 0; i < thirdParticleOnScreen.Length; i++)
            {
                if (thirdParticleOnScreen[i].transform.parent == null || thirdParticleOnScreen[i].transform.parent.name == "Third Marker")
                {
                    Destroy(thirdParticleOnScreen[i].gameObject);
                    thirdParticleOnScreen[i] = particle;
                    destroyed = true;
                    break;
                }
            }
            if (!destroyed)
            {
                Destroy(particle);
            }
            destroyed = false;
        }
    }
}

