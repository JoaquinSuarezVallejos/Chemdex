using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTransitioner : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;
    [SerializeField] private GameObject fadeBackground, ptableCanvas, introductionCanvas;

    public void StartTransition()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        fadeBackground.SetActive(true);
        transitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        ptableCanvas.SetActive(false);
        introductionCanvas.SetActive(true);
        transitionAnimator.SetTrigger("FadeIn");
    }
}
