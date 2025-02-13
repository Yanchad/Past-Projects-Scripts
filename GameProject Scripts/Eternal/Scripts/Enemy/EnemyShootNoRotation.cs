using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootNoRotation : MonoBehaviour
{
    [Header("Assignabled")]
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform bulletStartPos;

    [SerializeField] private float shootInterval = 2f;


    [Header("Shooting Obstacle")]
    [Tooltip("What is an obstacle between the player and the enemy")]
    [SerializeField] private LayerMask obstacleLayerMask;

    [Header("Audio")]
    [SerializeField] private GameObject enemyShootSound;

    // Privates
    private EnemyMove2_0 enemyMove;

    private float timer;



    private void Start()
    {
        enemyMove = GetComponentInParent<EnemyMove2_0>();
    }
    private void Update()
    {
        Shoot();
    }


    private void Shoot()
    {
        if (enemyMove != null)
        {
            timer += Time.deltaTime;

            if (timer > shootInterval)
            {
                Instantiate(enemyBullet, bulletStartPos.position, gun.rotation);

                if(enemyShootSound != null) Instantiate(enemyShootSound, bulletStartPos.position, Quaternion.identity); // Sound
                timer = 0f;
            }
        }
    }
}
