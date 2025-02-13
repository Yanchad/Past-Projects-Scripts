using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathMB : MonoBehaviour
{
    [SerializeField] private GameObject lootObject;
    [SerializeField] private GameObject deathFX;
    private EnemyHealth enemyHealth;

    [Header("Audio")]
    [SerializeField] private GameObject mbDeathSound;

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
            Instantiate(lootObject, transform.position, Quaternion.identity);
            if(mbDeathSound != null) Instantiate(mbDeathSound, transform.position, Quaternion.identity); // Sound
            Destroy(gameObject);
        }
    }
}
