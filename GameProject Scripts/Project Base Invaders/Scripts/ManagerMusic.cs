using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMusic : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource1;
    [SerializeField] private AudioClip musicClip1;
    [SerializeField] private AudioClip musicClip2;

    public static ManagerMusic instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource1.Play();
    }

    
    void Update()
    {
        if (!musicSource1.isPlaying)
        {
            musicSource1.clip = musicClip2;
            musicSource1.Play();
            if (!musicSource1.isPlaying)
            {
                musicSource1.clip = musicClip1;
                musicSource1.Play();
            }
        }
    }
}
