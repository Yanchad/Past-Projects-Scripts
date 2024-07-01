using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    WaveSpawner waveSpawner;


    [SerializeField] private float health;
    [SerializeField] private float shield;

    [SerializeField] private float damageToPlayer;
    public float DamageToPlayer { get { return damageToPlayer; } set { damageToPlayer = value; } }

    [SerializeField] private float damageToBase;
    public float DamageToBase { get { return damageToBase; }set { damageToBase = value; } }

    private bool shieldIsBroken;







    private void Start()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    void Update()
    {

        if (shield <= 0)
        {
            shieldIsBroken = true;
        }
        else shieldIsBroken = false;

        shield = Mathf.Clamp(shield, 0, 1000);
        health = Mathf.Clamp(health, 0, 1000);
        damageToPlayer = Mathf.Clamp(damageToPlayer, 0, 1000);
        damageToBase = Mathf.Clamp(damageToBase, 0, 1000);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Reduce the bullet's damage from Shield
        if (collision.gameObject.tag == "Bullet" && shieldIsBroken == false)
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            shield -= bullet.SHIELDDAMAGE;
        }
        //Reduce the bullet's damage from Health
        if (collision.gameObject.tag == "Bullet")
        {
            if (shieldIsBroken)
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();

                health -= bullet.HPDAMAGE;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
