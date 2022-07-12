using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenControl : MonoBehaviour
{
    public Image blackScreen;
    public Animator blackScreenAnimator;

    private void OnLevelWasLoaded(int level)
    {
        blackScreenAnimator.Play("Fade");

    }
}
