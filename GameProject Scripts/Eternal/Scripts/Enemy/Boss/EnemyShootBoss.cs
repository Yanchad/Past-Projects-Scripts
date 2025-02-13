using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShootBoss : MonoBehaviour
{
    [Header("Assignabled")]
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform bulletStartPos;

    [SerializeField] private float shootInterval = 2f;

    [Header("Shooting Obstacle")]
    [Tooltip("What is an obstacle between the player and the enemy")]
    [SerializeField] private LayerMask obstacleLayerMask;
    private float timer;

    [Header("Audio")]
    [SerializeField] private GameObject enemyShootSound;

    private Transform player;

    private Vector2 rotateDirection;

    private EnemyMove2_0 enemyMove;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject.transform;
        enemyMove = GetComponentInParent<EnemyMove2_0>();
    }

    private void Update()
    {
        Shoot();
    }
    private void Shoot()
    {
        if(enemyMove != null && HasLineOfSight())
        {
            RotateGun();
            timer += Time.deltaTime;

            if (timer > shootInterval)
            {
                timer = 0f;
                Instantiate(enemyBullet, bulletStartPos.position, Quaternion.identity);
                if (enemyShootSound != null) Instantiate(enemyShootSound, bulletStartPos.position, Quaternion.identity);
            }
        }
    }

    private void RotateGun()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - gun.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private bool HasLineOfSight()
    {
        // Cast a ray from gun to the player to check for obstacles
        Vector2 direction = player.position - gun.position;
        float distance = direction.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(gun.position, direction.normalized, distance, obstacleLayerMask);

        // Return true if the raycast doesn't hit an obstacle
        return hit.collider == null;
    }
}
