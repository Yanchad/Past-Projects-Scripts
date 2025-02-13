using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private GameObject deathFX;
    [Header("Audio")]
    [SerializeField] private GameObject deathSound;
    EnemyHealth enemyHealth;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (enemyHealth.IsDead)
        {
            Instantiate(deathFX, transform.position, Quaternion.identity);
            Instantiate(deathSound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
