using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] SpeedrunTimer speedRunTimer;
    [SerializeField] TextMeshProUGUI endText;
    ActivateTimer activateTimer;


    void Start()
    {
        activateTimer = FindObjectOfType<ActivateTimer>();
    }

    
    void Update()
    {
        EndText();
    }


    public void EndText()
    {
        if (activateTimer.TimerActive == true) endText.text = String.Format("Congratulations, you have escaped! \n Final time is: {0}", speedRunTimer.timerText.text);
        else endText.text = String.Format("Congratulations, you have escaped!");
    }

    public void ExitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        SpeedrunTimer.Instance.watch.Reset();
    }

}
