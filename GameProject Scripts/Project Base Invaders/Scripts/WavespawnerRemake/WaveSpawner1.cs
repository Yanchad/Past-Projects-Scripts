using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner1 : MonoBehaviour
{

    [Header("Wave Info")]
    [SerializeField] private bool waveStarted;
    [SerializeField] private bool waveEnded;
    [SerializeField] private bool wavePaused;
    [SerializeField] private int spawnedEnemiesTotal;
    [SerializeField] private int spawnedEnemiesCurrentWave;
    [SerializeField] private int enemiesLeft;
    [SerializeField] private int currentWaveIndex;

    [Header("General Settings")]
    [Tooltip("Set whether the wave starts after all enemies has died + wave activation time or only wave activation time")]
    [SerializeField] private bool nextWaveWhenNoEnemiesLeft;

    [Header("SpawnPoint Setup")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Wave Setup")]
    public Wave1[] waves;

    private List<GameObject> aliveEnemies = new List<GameObject>();
    
    private bool skipWaveStartTime;



    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        foreach (Wave1 wave in waves)
        {

            if (!wavePaused)
            {
                // If skipWaveStartTime is true, set the waveStartTime to 0
                float currentWaveStartTime = (skipWaveStartTime) ? 0f : wave.waveStartTime;

                // Spawn the current wave and wait for the specified time before the next one
                yield return new WaitForSeconds(currentWaveStartTime);

                currentWaveIndex++;
                // Reset the count for the current wave
                spawnedEnemiesCurrentWave = 0;

                yield return StartCoroutine(SpawnEnemies(wave));

                // if "NextWaveWhenNoEnemiesLeft" is enabled. Wait until all enemies of the current wave are destroyed to spawn next wave
                if (nextWaveWhenNoEnemiesLeft == true)
                {
                    while (enemiesLeft > 0)
                    {
                        yield return null;
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator SpawnEnemies(Wave1 wave)
    {
        foreach (EnemyType enemyType in wave.enemies)
        {
            // Randomly choose a spawn point from the array
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            waveStarted = true;

            // Instantiate the specified number of enemies for the current type
            for (int i = 0; i < enemyType.EnemyCount; i++)
            {
                // Instantiate enemies based on the enemyType
                GameObject enemy = Instantiate(enemyType.EnemyPrefab[Random.Range(0, enemyType.EnemyPrefab.Length)], spawnPoint.position, Quaternion.identity);

                // Add the spawned enemy to the list of alive enemies
                aliveEnemies.Add(enemy);

                // Subscribe to OnDetroyed event to track enemy destruction
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.OnDestroyed += () => UpdateDestroyedEnemyCount(enemy);
                }

                spawnedEnemiesTotal++;
                spawnedEnemiesCurrentWave++;

                yield return new WaitForSeconds(wave.enemySpawnInterval);
            }
        }
    }

    private void Update()
    {
        // Check for inactive enemies and update the count
        aliveEnemies.RemoveAll(enemy => enemy == null || !enemy.activeSelf);
        enemiesLeft = aliveEnemies.Count;

        if (enemiesLeft <= 0)
        {
            waveEnded = true;
            waveStarted = false;
        }else waveEnded = false;
    }


    private void UpdateDestroyedEnemyCount(GameObject enemy)
    {
        // Remove the destroyed enemy from the list of alive enemies
        aliveEnemies.Remove(enemy);
        // Update the count of enemies left
        enemiesLeft = aliveEnemies.Count;
    }

    // Call this Method to pause the wave
    public void PauseWave()
    {
        wavePaused = !wavePaused;
    }

    // Call this Method to skip the Wave start time
    public void SkipWaveStartTime()
    {
        skipWaveStartTime = true;
    }
}


[System.Serializable]
public class Wave1
{
    [Header("Time Settings")]
    [Tooltip("Time Delay Before activating this wave")] public float waveStartTime;
    [Tooltip("Spawn Delay between each enemy")] public float enemySpawnInterval; 

    [Header("Enemy types")]
    public EnemyType[] enemies;
}



[System.Serializable]
public class EnemyType
{
    public GameObject[] EnemyPrefab;
    public int EnemyCount;
}
