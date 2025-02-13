using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Rigidbody2D rb;
    private float timer;

    [SerializeField] private float bulletDespawnTime;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float damage;
    
    [Header("Effects")]
    [SerializeField] private GameObject hitFX;
    [SerializeField] private GameObject shieldHitLight;

    [Header("Audio")]
    [SerializeField] private GameObject shieldHitSound;

    public float Damage { get { return damage; } set { damage = value; } }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
        player = GameObject.Find("Player").gameObject;

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation); // + z axis rotation to make the bullet face the correct way
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
            if(shieldHitLight != null) Instantiate(shieldHitLight, transform.position, Quaternion.identity);
            Debug.Log("PlayerShield Hit");
        }
    }
}
