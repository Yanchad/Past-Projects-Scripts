using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldBoss : MonoBehaviour
{
    [Header("Assignabled")]
    [SerializeField] private Transform shield;
    [SerializeField] private Collider2D shieldCollider;
    [SerializeField] private SpriteRenderer shieldRenderer;

    [SerializeField] private float shieldCooldown;
    [SerializeField] private float shieldDuration;
    [Header("Shield Aiming Obstacle")]
    [Tooltip("What is an obstacle between the player and the enemy")]
    [SerializeField] private LayerMask obstacleLayerMask;

    private Transform player;

    private float timer;
    private float timer2;
    private Vector2 rotateDirection;

    private EnemyMove2_0 enemyMove;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject.transform;
        enemyMove = GetComponentInParent<EnemyMove2_0>();
    }

    private void Update()
    {
        toggleShield();
        RotateChecks();
    }
    private void toggleShield()
    {
        timer += Time.deltaTime;

        if (timer >= shieldCooldown)
        {
            shieldCollider.enabled = true;
            shieldRenderer.enabled = true;
            timer2 += Time.deltaTime;
            if(timer2 >= shieldDuration)
            {
                timer2 = 0f;
                timer = 0f;
            }
        }
        else 
        {
            shieldCollider.enabled = false;
            shieldRenderer.enabled = false;
        }

    }
    private void RotateChecks()
    {
        if (enemyMove != null && HasLineOfSight())
        {
            RotateGun();
        }
    }

    private void RotateGun()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - shield.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (-90f);

            shield.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private bool HasLineOfSight()
    {
        // Cast a ray from gun to the player to check for obstacles
        Vector2 direction = player.position - shield.position;
        float distance = direction.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(shield.position, direction.normalized, distance, obstacleLayerMask);

        // Return true if the raycast doesn't hit an obstacle
        return hit.collider == null;
    }
}
