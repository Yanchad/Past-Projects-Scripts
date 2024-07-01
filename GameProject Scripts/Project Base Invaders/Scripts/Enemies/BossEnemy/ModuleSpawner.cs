using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ModuleSpawner : MonoBehaviour
{

    BossMove bossMove;
    ModuleActiveCheck moduleActiveCheck;

    [Header("Enemies to spawn")]
    [SerializeField] private GameObject[] motherShipEnemies;

    [Header("Enemy spawn Time Settings")]
    [SerializeField] private float waitTimeTillSpawn;
    [SerializeField] private float enemySpawnInterval;

    private float timer1;
    private float timer2;

    [SerializeField] private bool isSpawning;
    
    private bool readyToSpawn;
    private bool mothershipSpawning;

    public bool MothershipSpawning => mothershipSpawning;
    
    void Start()
    {
        bossMove = GetComponent<BossMove>();
        moduleActiveCheck = GetComponent<ModuleActiveCheck>();
        timer1 = waitTimeTillSpawn;
        timer2 = enemySpawnInterval;
    }

    
    void Update()
    {
        SpawnTimings();
        if(moduleActiveCheck.NoModulesLeft == false) StartCoroutine(ModuleSpawn());
    }

    private void SpawnTimings()
    {
        if (bossMove.HasReachedTarget)
        {
            timer1 -= Time.deltaTime;

            if (timer1 <= 0)
            {
                isSpawning = true;
                timer1 = 0;
            } 
            else isSpawning = false;

            mothershipSpawning = true;
        } else mothershipSpawning = false;

        if (isSpawning)
        {
            timer2 -= Time.deltaTime;

            if(timer2 <= 0)
            {
                readyToSpawn = true;
                timer2 = enemySpawnInterval;
            } 
            else readyToSpawn = false;
        }
    }

    private IEnumerator ModuleSpawn()
    {
        if (isSpawning)
        {
            int enemyIndex = Random.Range(0, motherShipEnemies.Length);
            GameObject currentEnemy = motherShipEnemies[enemyIndex];

            if (readyToSpawn)
            {
                Instantiate(currentEnemy, transform.position, Quaternion.identity);
            }
            yield return null;
        }
    }
}
