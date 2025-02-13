using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;

    private bool isDead = false;
    public float Health => health;

    public bool IsDead => isDead;

    private List<IOnEnemyTakeDamage> OnTakeDamageEnemy = new List<IOnEnemyTakeDamage>();

    public void RegisterDamageListener(IOnEnemyTakeDamage listener)
    {
        OnTakeDamageEnemy.Add(listener);
    }
    public void RemoveDamageListener(IOnEnemyTakeDamage listener)
    {
        OnTakeDamageEnemy.Remove(listener);
    }
    public void InvokeDamageTake(float hp)
    {
        for (int i = 0; i < OnTakeDamageEnemy.Count; i++)
        {
            OnTakeDamageEnemy[i].OnTakeDamageEnemy(hp);
        }
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, 1000f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            health -= bullet.Damage;
            InvokeDamageTake(health);
            if (health <= 0)
            {
                isDead = true;
            }
        }
    }
}
