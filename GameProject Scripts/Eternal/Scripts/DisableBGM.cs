using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBGM : MonoBehaviour
{
    private Animator bgmAnimator;

    private void Awake()
    {
        bgmAnimator = GameObject.Find("BackgroundMusic").GetComponent<Animator>();
    }

    void Start()
    {
        if(bgmAnimator != null) bgmAnimator.SetTrigger("AudioFadeOut");
    }
}
