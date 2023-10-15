using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomVFX : MonoBehaviour
{
    public void PlayVFX()
    {
        StartCoroutine(clickedOn());
    }

    IEnumerator clickedOn()
    {
        yield return new WaitForSeconds(0.17f);
        ParticleSystem atomVFX = GameObject.Find("AtomVFX").GetComponent<ParticleSystem>();
        atomVFX.Play();
    }
}
