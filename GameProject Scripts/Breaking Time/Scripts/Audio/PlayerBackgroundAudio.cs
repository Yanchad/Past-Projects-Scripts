using UnityEngine;

public class PlayerBackgroundAudio : MonoBehaviour
{
    [SerializeField] private AudioSource windAudio;

    void Start()
    {
        windAudio.Play();
    }
}
