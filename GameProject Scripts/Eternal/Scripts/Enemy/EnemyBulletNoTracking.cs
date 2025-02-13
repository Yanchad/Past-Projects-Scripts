using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletNoTracking : MonoBehaviour
{

    [SerializeField] private float bulletDespawnTime;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float damage;

    [Header("Effects")]
    [SerializeField] private GameObject hitFX;
    [SerializeField] private GameObject shieldHitLight;

    [Header("Audio")]
    [SerializeField] private GameObject shieldHitSound;
    
    private Rigidbody2D rb;
    private float timer;

    // Getters & Setters
    public float Damage { get { return damage; } set { damage = value; } }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * projectileSpeed;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > bulletDespawnTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Wall")
            || collision.CompareTag("Bullet"))
        {
            Instantiate(hitFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D hitCollider = collision.collider;

        if (hitCollider.CompareTag("PlayerShield"))
        {
            if (shieldHitSound != null) Instantiate(shieldHitSound, transform.position, Quaternion.identity); // Sound
            if (shieldHitLight != null) Instantiate(shieldHitLight, transform.position, Quaternion.identity); // Light
            Debug.Log("PlayerShield Hit");
        }
    }
}
