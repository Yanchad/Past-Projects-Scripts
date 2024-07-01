using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayBG1;
    [SerializeField] private GameObject howToPlayBG2;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;


    public void Play()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1.0f;
        audioSource2.Play();
    }

    public void Exit()
    {
        Application.Quit();
        audioSource2.Play();
    }
    public void HowToPlay()
    {
        howToPlayBG1.SetActive(true);
        audioSource2.Play();
    }

    public void HowToPlayNext()
    {
        howToPlayBG1.SetActive(false);
        howToPlayBG2.SetActive(true);
        audioSource2.Play();
    }
    public void HowToPlayBack()
    {
        howToPlayBG2.SetActive(false);
        howToPlayBG1.SetActive(true);
        audioSource2.Play();
    }
    public void CloseHowToPlay()
    {
        howToPlayBG1.SetActive(false);
        howToPlayBG2.SetActive(false);
        audioSource2.Play();
    }
    public void BossTest()
    {
        SceneManager.LoadScene("FinalBoss");
        audioSource2.Play();
    }



    //HOVER
    public void HoverSound()
    {
        audioSource.Play();
    }
}
