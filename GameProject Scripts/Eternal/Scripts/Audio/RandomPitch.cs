using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    [SerializeField] private float pitchMinValue;
    [SerializeField] private float pitchMaxValue;
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(pitchMinValue, pitchMaxValue);
    }
}
