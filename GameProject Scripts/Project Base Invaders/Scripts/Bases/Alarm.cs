using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Alarm : MonoBehaviour
{
    Greenhouse greenHouse;
    ResearchLab researchLab;
    H3Mine h3Mine;
    Mechanic mechanic;
    WaveSpawner waveSpawner;

    [SerializeField] private List<GameObject> baseAlarms = new List<GameObject>();

    [Header("Alarm time settings")]
    [SerializeField] private float minTime = 10;
    [SerializeField] private float maxTime = 20;
    [SerializeField] private float firstAlarmTime;
    [SerializeField] private float randomTime;

    private int randomOption;
    private bool anyAlarmActive;

    [Header("Checking if Active")]
    [SerializeField] public GameObject greenhouseAlarm;
    [SerializeField] public GameObject researchLabAlarm;
    [SerializeField] public GameObject h3MineAlarm;
    [SerializeField] public GameObject mechanicAlarm;

    public float RandomTime => randomTime;
    public bool AnyAlarmActive => anyAlarmActive;


    private void Awake()
    {
        greenHouse = FindObjectOfType<Greenhouse>();
        researchLab = FindObjectOfType<ResearchLab>();
        h3Mine = FindObjectOfType<H3Mine>();
        mechanic = FindObjectOfType<Mechanic>();
        waveSpawner = FindObjectOfType<WaveSpawner>();


        randomTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        DisableAlarmsAfterBoss();
        DisableDestroyedAlarms();
        if (researchLabAlarm.activeInHierarchy || greenhouseAlarm.activeInHierarchy || h3MineAlarm.activeInHierarchy || mechanicAlarm.activeInHierarchy)
        {
            waveSpawner.ReadyToCountDown = true;
            anyAlarmActive = true;
        }
        else 
        {
            waveSpawner.ReadyToCountDown = false;
            anyAlarmActive = false;
        }


        firstAlarmTime -= Time.deltaTime;

        if (firstAlarmTime <= 0)
        {
            firstAlarmTime = 0;
        }

        if(!greenhouseAlarm.activeInHierarchy && !researchLabAlarm.activeInHierarchy && !h3MineAlarm.activeInHierarchy && !mechanicAlarm.activeInHierarchy && firstAlarmTime <= 0)
        {
            randomTime -= Time.deltaTime;
        }

        if (greenHouse.IsDestroyed || researchLab.IsDestroyed || h3Mine.IsDestroyed || mechanic.IsDestroyed)
        {
            RemoveNullAlarm();
        }
        if (randomTime <= 0)
        {
            ChooseAlarm();
            ActivateAlarm();
        }
        if (waveSpawner.SpawnedEnemies == waveSpawner.CurrentWaveLength) DisableAlarm();

    }

    private void DisableDestroyedAlarms()
    {
        if (greenHouse.IsDestroyed && greenhouseAlarm.activeInHierarchy) DisableAlarm();
        if (h3Mine.IsDestroyed && h3MineAlarm.activeInHierarchy) DisableAlarm();
        if (researchLab.IsDestroyed && researchLabAlarm.activeInHierarchy) DisableAlarm();
        if (mechanic.IsDestroyed && mechanicAlarm.activeInHierarchy) DisableAlarm();
    }
    private void DisableAlarmsAfterBoss()
    {
        if(waveSpawner.CurrentWaveIndex >= waveSpawner.waves.Length)
        {
            DisableAlarm();
        }
    }
    private void ActivateAlarm()
    {
         baseAlarms[randomOption].SetActive(true);

    }
    public void DisableAlarm()
    {
        for (int i = 0; i < baseAlarms.Count; i++)
        {
            baseAlarms[i].SetActive(false);
        }
    }
    private void RemoveNullAlarm()
    {
        if (greenHouse.IsDestroyed) baseAlarms.Remove(greenhouseAlarm);
        if(h3Mine.IsDestroyed) baseAlarms.Remove(h3MineAlarm);
        if(researchLab.IsDestroyed) baseAlarms.Remove(researchLabAlarm);
        if (mechanic.IsDestroyed) baseAlarms.Remove(mechanicAlarm);
        baseAlarms.RemoveAll(s => s == null);
    }
    private void ChooseAlarm()
    {
        if(!greenhouseAlarm.activeInHierarchy && !researchLabAlarm.activeInHierarchy && !h3MineAlarm.activeInHierarchy && !mechanicAlarm.activeInHierarchy)
        {
            randomOption = Random.Range(0, baseAlarms.Count);
            randomTime = Random.Range(minTime, maxTime);
        }

    }
}
