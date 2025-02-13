using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDespawnTime;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float damage;
    
    [Header("Effects")]
    [SerializeField] private GameObject hitFX;
    [SerializeField] private GameObject shieldHitLight;

    [Header("Audio")]
    [SerializeField] private GameObject shieldHitSound;
    [SerializeField] private GameObject hitSound;
    
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    private GameObject player;
    private float timer;

    // Getters & Setters
    public float Damage => damage;


    private void Awake()
    {
        player = GameObject.Find("Player").gameObject;
    }

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();

        // Get the mouse position and set the direction and rotation
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = player.transform.position - transform.position;
        Vector3 rotation = transform.position - mousePos;

        // Send the bullet flying
        rb.velocity = new Vector2(-direction.x, -direction.y).normalized * projectileSpeed;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > bulletDespawnTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy") || collider.CompareTag("EnemyBullet"))
        {
            Instantiate(hitFX, transform.position, Quaternion.identity);
            Instantiate(hitSound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D hitCollider = collision.collider;

        if (hitCollider.CompareTag("EnemyShield"))
        {
            if (shieldHitSound != null) Instantiate(shieldHitSound, transform.position, Quaternion.identity);
            if (shieldHitLight != null) Instantiate(shieldHitLight, transform.position, Quaternion.identity);
            Debug.Log("EnemyShield Hit");
        }
    }
}
