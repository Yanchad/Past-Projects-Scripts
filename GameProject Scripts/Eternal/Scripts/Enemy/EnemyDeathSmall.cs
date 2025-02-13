using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSmall : MonoBehaviour
{
    [SerializeField] private GameObject deathFX;

    [Header("Audio")]
    [SerializeField] private GameObject deathSound;
    private EnemyHealth enemyHealth;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        noteActivation();
    }
    private void noteActivation()
    {
        if(enemyHealth.IsDead)
        {
            Instantiate(deathFX, transform.position, Quaternion.identity);
            if(deathSound != null) Instantiate(deathSound, transform.position, Quaternion.identity); // Sound
            Destroy(gameObject);
        }
    }
}
