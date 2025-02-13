using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathBoss : MonoBehaviour
{
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject deathFX;
    [SerializeField] private GameObject gate;
    EnemyHealth enemyHealth;

    [Header("Audio")]
    [SerializeField] private GameObject deathSound;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        DeathActivation();
    }
    private void DeathActivation()
    {
        if (enemyHealth.IsDead)
        {
            Instantiate(deathFX, transform.position, Quaternion.identity);
            Instantiate(gate, transform.position, Quaternion.identity);
            if (deathSound != null) Instantiate(deathSound, transform.position, Quaternion.identity); // Sound
            Destroy(gameObject);
        }
    }
    private IEnumerator delayGateSpawn()
    {
        yield return new WaitForSeconds(2f);
    }
}
