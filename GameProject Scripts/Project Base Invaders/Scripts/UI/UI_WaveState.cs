using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_WaveState : MonoBehaviour
{
    WaveSpawner waveSpawner;
    Alarm alarm;

    [SerializeField] TextMeshProUGUI waveStatetxt;
    [SerializeField] GameObject waveStateGO;
    private float currentWave;

    void Awake()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        alarm = FindObjectOfType<Alarm>();
    }

    
    void Update()
    {
        if(currentWave >= 15)
        {
            waveStateGO.SetActive(false);
        }
        if (waveSpawner.CurrentWaveIndex < waveSpawner.waves.Length)
        {
            GetWave();
            SetWaveTxt();
        }
    }

    private void GetWave()
    {
        currentWave = waveSpawner.CurrentWaveIndex + 1;
    }
    private void SetWaveTxt()
    {

        if(alarm.greenhouseAlarm.activeInHierarchy || alarm.h3MineAlarm.activeInHierarchy || alarm.researchLabAlarm.activeInHierarchy || alarm.mechanicAlarm.activeInHierarchy)
        {
            waveStatetxt.text = "WAVE " + currentWave;
        }

    }
}
