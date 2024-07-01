using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    Fuel fuel;
    [SerializeField] private GameObject shopWindow;
    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private GameObject gameWinWindow;
    [SerializeField] private GameObject startPopupWindow;
    [SerializeField] private GameObject pauseWindow;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSourceThruster;
    [SerializeField] private AudioSource audioSouceSideThrusterA;
    [SerializeField] private AudioSource audioSouceSideThrusterB;

    void Start()
    {
        fuel = GetComponent<Fuel>();
    }

    
    void Update()
    {
        playEngineAudio();
    }
    private void playEngineAudio()
    {
        if (fuel.FuelEmpty == false && startPopupWindow.activeInHierarchy == false && gameOverWindow.activeInHierarchy == false && gameWinWindow.activeInHierarchy == false
            && shopWindow.activeInHierarchy == false && pauseWindow.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                audioSourceThruster.volume = 0.35f;
                audioSourceThruster.Play();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                audioSourceThruster.volume = 0f;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                audioSouceSideThrusterA.volume = 0.20f;
                audioSouceSideThrusterA.Play();
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                audioSouceSideThrusterA.volume = 0f;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                audioSouceSideThrusterB.volume = 0.20f;
                audioSouceSideThrusterB.Play();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                audioSouceSideThrusterB.volume = 0f;
            }
        }
        else 
        {
            audioSourceThruster.Stop();
            audioSouceSideThrusterA.Stop();
            audioSouceSideThrusterB.Stop();
        } 
    }
}
