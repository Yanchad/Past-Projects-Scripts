using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    private float timer;

    [SerializeField] private float bulletDespawnTime;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float hpDamage;
    [SerializeField] private float shieldDamage;

    public float HPDAMAGE { get { return hpDamage; }set { hpDamage = value; } }
    public float SHIELDDAMAGE { get {  return shieldDamage; }set {  shieldDamage = value; } }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(Time.timeScale == 1f)
        {
            player = GameObject.Find("Player").gameObject;
        }
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation); // + z axis rotation to make the bullet face the correct way
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > bulletDespawnTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "EnemyBullet" && collision.gameObject.tag != "Building" && collision.gameObject.tag != "PatrolPoint" && collision.gameObject.tag != "PatrolPointStart" 
            && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Planet" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Barrel" && collision.gameObject.tag != "Mothership")
        {
            Destroy(gameObject);
        }
    }
}
