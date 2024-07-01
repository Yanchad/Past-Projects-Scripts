using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    Controls controls;

    [Tooltip("Set the Spawn delay after an alarm has activated")]
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject spawnPoint;

    [Tooltip("Wave spawn countdown timer")]
    [SerializeField] private float timer;
    [SerializeField] public Wave[] waves;
    [SerializeField] private int currentWaveIndex = 0;

    private bool readyToCountDown;
    
    [SerializeField] private int currentWaveLenght;
    [SerializeField] private int spawnedEnemies;
    [SerializeField]private bool enemiesNotLeft;
    public int CurrentWaveIndex => currentWaveIndex;
    public int SpawnedEnemies => spawnedEnemies;
    public float CurrentWaveLength => currentWaveLenght;
    public bool ReadyToCountDown {  get { return readyToCountDown; }set { readyToCountDown = value; } }
    public bool EnemiesNotLeft => enemiesNotLeft;



    void Start()
    {
        controls = FindObjectOfType<Controls>();
        timer = spawnDelay;
        currentWaveLenght = waves[currentWaveIndex].enemies.Length;
    }


    void Update()
    {

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("You survived every wave");
            return;
        }

        if(readyToCountDown == true)
        {
            timer -= Time.deltaTime;
        }
    
        if(timer <= 0)
        {
            readyToCountDown = false;
            StartCoroutine(SpawnWave());
            timer = spawnDelay;
        }
    }



    private IEnumerator SpawnWave()
    {
        currentWaveLenght = waves[currentWaveIndex].enemies.Length;
        if (spawnedEnemies <= 0)
        {
            if (currentWaveIndex < waves.Length)
            {
                if (spawnedEnemies < currentWaveLenght)
                {
                    for (int i = 0; i < currentWaveLenght; i++)
                    {
                        Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform.position, Quaternion.Euler(0, 0, 0));
                        spawnedEnemies++;
                        yield return new WaitForSeconds(waves[currentWaveIndex].enemySpawnInterval);
                    }
                    if(currentWaveIndex <= waves.Length)currentWaveIndex++;
                    spawnedEnemies = 0;
                }
            }
        }
    }
}


[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float enemySpawnInterval;
}
