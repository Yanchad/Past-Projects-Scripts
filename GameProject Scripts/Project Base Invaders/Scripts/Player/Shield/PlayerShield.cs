using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            EnemyBullet enemyBullet = collision.gameObject.GetComponent<EnemyBullet>();

            playerHealth.Shield -= enemyBullet.SHIELDDAMAGE;
            Destroy(enemyBullet.gameObject);
        }
    }
}
