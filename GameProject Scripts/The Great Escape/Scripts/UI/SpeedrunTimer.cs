using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class SpeedrunTimer : MonoBehaviour
{

    public static SpeedrunTimer Instance;
    public Stopwatch watch;
    public TextMeshProUGUI timerText;
    public TimeSpan currentTime { get; private set; }


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        watch = new Stopwatch();
    }

    private void Update()
    {
        currentTime = watch.Elapsed;
        float currentMilliseconds = currentTime.Milliseconds;
        //string currentMillisecondsString = String.Format("{0,3D}", currentMilliseconds);

        int currentSeconds = currentTime.Seconds;
        //string currentSecondsString = String.Format("{0,2D}", currentSeconds);

        int currentMinutes = currentTime.Minutes;

        timerText.text = String.Format("{0}:{1:00}:{2:000}", currentMinutes, currentSeconds, currentMilliseconds);
    }
}
