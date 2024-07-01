using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchLab : MonoBehaviour
{
    WaveSpawner waveSpawner;
    
    [SerializeField] private float health;
    [SerializeField] private bool isDestroyed;

    public float Health { get { return health; } set { health = value; } }
    public bool IsDestroyed => isDestroyed;


    private void Awake()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    void Update()
    {
        if (health <= 0)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
        else isDestroyed = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            health -= enemy.DamageToBase;

            Destroy(enemy.gameObject);
        }
    }
}
