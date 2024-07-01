using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip musicClip1;
    [SerializeField] private AudioClip musicClip2;

    public static MusicManager instance;
    private Scene currentScene;

    private bool isTransitioning = false;
    [SerializeField] private float transitionDuration;


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == "MainMenu" && musicSource.clip != musicClip1 && !isTransitioning)
        {
            StartCoroutine(MusicTransition(musicClip1));
        }
        else if(currentScene.name == "GameScene" && musicSource.clip != musicClip2 && !isTransitioning)
        {
            StartCoroutine(MusicTransition(musicClip2));
        }
    }

    private void PlayMusic()
    {
        if(currentScene.name == "MainMenu")
        {
            musicSource.clip = musicClip1;
        }
        else if(currentScene.name == "GameScene")
        {
            musicSource.clip = musicClip2;
        }

        musicSource.Play();
    }

    IEnumerator MusicTransition(AudioClip targetClip)
    {
        isTransitioning = true;

        // Get the initial volume
        float startVolume = musicSource.volume;

        // Fade out current music
        float elapsedTime = 0f;

        while(elapsedTime < transitionDuration)
        {
            // Calculate the interpolation factor using an ease-in-out curve
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration);

            // Interpolate the volume
            musicSource.volume = Mathf.Lerp(startVolume, 0, t);

            // increment the elapsed time
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        musicSource.Stop();

        // Switch to new music clip
        musicSource.clip = targetClip;
        musicSource.Play();

        // Fade in new music
        elapsedTime = 0f;
        while(elapsedTime < transitionDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration);

            musicSource.volume = Mathf.Lerp(0f, startVolume, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure volume is set to the initial volume
        musicSource.volume = startVolume;

        isTransitioning = false;
    }
}
