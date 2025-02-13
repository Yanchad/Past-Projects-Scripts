using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Rigidbody2D rb;
    PlayerMove playerMove;
    PlayerShoot playerShoot;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deathFX;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private bool isDead;

    [Header("Hide On Death")]
    [SerializeField] private GameObject[] objectsToHideOnDeath;

    [Header("Audio")]
    [SerializeField] private GameObject takeDamageSound;
    [SerializeField] private GameObject deathSound;

    public float Health {  get { return health; } set { health = value; } }
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public bool IsDead => isDead;

    private List <IOnTakeDamage> OnTakeDamage = new List<IOnTakeDamage>();
    private List<IOnGameOver> OnGameOver = new List<IOnGameOver>();
    private List<IOnHealHp> OnHealhp = new List<IOnHealHp>();

    #region LISTENERS
    public void RegisterDeathListener(IOnGameOver listener)
    {
        OnGameOver.Add(listener);
    }
    public void RemoveDeathListener(IOnGameOver listener)
    {
        OnGameOver.Remove(listener);
    }
    public void InvokeDeath(bool isDead)
    {
        for (int i = 0; i < OnGameOver.Count; i++)
        {
            OnGameOver[i].OnGameOver(isDead);
        }
    }
    public void RegisterDamageListener(IOnTakeDamage listener)
    {
        OnTakeDamage.Add(listener);
    }
    public void RemoveDamageListener(IOnTakeDamage listener)
    {
        OnTakeDamage.Remove(listener);
    }
    public void InvokeDamageTake(float hp)
    {
        for (int i = 0; i < OnTakeDamage.Count; i++)
        {
            OnTakeDamage[i].OnTakeDamage(hp);
        }
    }
    public void RegisterHealListener(IOnHealHp listener)
    {
        OnHealhp.Add(listener);
    }
    public void RemoveHealListener(IOnHealHp listener)
    {
        OnHealhp.Remove(listener);
    }
    public void InvokeHeal(float hp)
    {
        for(int i = 0;i < OnHealhp.Count; i++)
        {
            OnHealhp[i].OnHealHp(hp);
        }
    }
    #endregion LISTENERS


    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerShoot = GetComponent<PlayerShoot>();
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        //ShowObjects();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            EnemyBullet enemyBullet = collision.gameObject.GetComponent<EnemyBullet>();

            health -= enemyBullet.Damage;
            Instantiate(takeDamageSound, transform.position, Quaternion.identity); //Get Hit Sound
            InvokeDamageTake(health / maxHealth);
            Destroy(enemyBullet.gameObject);
            if (health <= 0)
            {
                isDead = true;
                circleCollider.enabled = false; // Disable additional death effects
                boxCollider.enabled = false;
                Instantiate(deathFX, transform.position, Quaternion.identity); // DeathFX
                HideObjects();  // Hide objects on death to hide the player
                playerMove.enabled = false;
                playerShoot.enabled = false;
                InvokeDeath(isDead);
                if (deathSound != null) Instantiate(deathSound, transform.position, Quaternion.identity);
            }
        }
        if (collision.CompareTag("EnemyBulletNoTracking"))
        {
            EnemyBulletNoTracking enemyBullet = collision.gameObject.GetComponent<EnemyBulletNoTracking>();

            health -= enemyBullet.Damage;
            Instantiate(takeDamageSound, transform.position, Quaternion.identity); //Get Hit Sound
            InvokeDamageTake(health / maxHealth);
            Destroy(enemyBullet.gameObject);
            if (health <= 0)
            {
                isDead = true;
                circleCollider.enabled = false; // Disable additional death effects
                boxCollider.enabled = false;
                Instantiate(deathFX, transform.position, Quaternion.identity); // DeathFX
                HideObjects(); // Hide objects on death to hide the player
                playerMove.enabled = false;
                playerShoot.enabled = false;
                InvokeDeath(isDead);
                if (deathSound != null) Instantiate(deathSound, transform.position, Quaternion.identity);
            }
        }
        if (collision.CompareTag("Hint1"))
        {
            if (health < maxHealth)
            {
                health += 1;
                InvokeHeal(health / maxHealth);
            }
        }
        if (collision.CompareTag("Hint2"))
        {
            if (health < maxHealth)
            {
                health += 1;
                InvokeHeal(health / maxHealth);
            }
        }
        if (collision.CompareTag("Hint3"))
        {
            if (health < maxHealth)
            {
                health += 1;
                InvokeHeal(health / maxHealth);
            }
        }
        if (collision.CompareTag("Hint4"))
        {
            if (health < maxHealth)
            {
                health += 1;
                InvokeHeal(health / maxHealth);
            }
        }
    }

    private void HideObjects()
    {
        if (objectsToHideOnDeath != null)
        {
            foreach (GameObject go in objectsToHideOnDeath)
            {
                if(go.activeInHierarchy && go != null)
                {
                    go.SetActive(false);
                }
            }
        }
    }
    private void ShowObjects()
    {
        if (objectsToHideOnDeath != null)
        {
            foreach (GameObject go in objectsToHideOnDeath)
            {
                go.SetActive(true);
            }
        }
    }
}
