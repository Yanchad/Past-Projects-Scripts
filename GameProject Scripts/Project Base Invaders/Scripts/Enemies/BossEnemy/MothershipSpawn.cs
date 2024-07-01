using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipSpawn : MonoBehaviour
{
    WaveSpawner waveSpawner;
    Alarm alarm;

    [SerializeField] private GameObject mothership;

    private bool mothershipSpawned;


    void Start()
    {
        waveSpawner = GetComponent<WaveSpawner>();
        alarm = FindObjectOfType<Alarm>();
        mothershipSpawned = false;
    }

    
    void Update()
    {
        Spawn();
        
    }

    private void Spawn()
    {
        if (waveSpawner.CurrentWaveIndex >= waveSpawner.waves.Length - 1 && !mothershipSpawned && alarm.AnyAlarmActive)
        {
            Instantiate(mothership, this.transform.position, Quaternion.identity);
            mothershipSpawned = true;
        }
    }
}
