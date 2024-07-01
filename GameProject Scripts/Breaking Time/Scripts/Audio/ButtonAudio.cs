using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{

    [SerializeField] private AudioSource buttonHover;
    [SerializeField] private AudioSource buttonPress;

    public void ButtonHoverAudio()
    {
        buttonHover.Play();
    }
    public void ButtonPressAudio()
    {
        buttonPress.Play();
    }
}
