using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHandlerScore : MonoBehaviour
{
    [SerializeField] AudioSource creditsAudioSource;

    void Start()
    {
        creditsAudioSource.Play();
        Destroy(gameObject, 1f);
        transform.localPosition += new Vector3(-0.4f, 0, 0);
    }
}
