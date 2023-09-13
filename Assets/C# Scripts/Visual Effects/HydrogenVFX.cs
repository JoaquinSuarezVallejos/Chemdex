using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrogenVFX : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(clickedOn());
    }

    IEnumerator clickedOn()
    {
        yield return new WaitForSeconds(0.18f);
        ParticleSystem hydrogenVFX = GameObject.Find("HydrogenVFX").GetComponent<ParticleSystem>();
        hydrogenVFX.Play();
    }
}
