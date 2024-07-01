using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Greenhouse greenHouse;
    H3Mine h3Mine;
    ResearchLab researchLab;
    Mechanic mechanic;
    Controls controls;
    GroundCheck groundCheck;

    private Rigidbody2D rb;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float shield;
    [SerializeField] private float maxShield;
    [SerializeField] private GameObject shieldGO;
    private float moveSpeed;
    private bool shieldIsBroken;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;

    public float Health { get { return health; } set { health = value; } }
    public float MaxHealth { get { return maxHealth; }set { maxHealth = value; } }
    public float MaxShield { get { return maxShield; } set { maxShield = value; } }
    public float Shield { get { return shield; } set { shield = value; } }
    public bool ShieldIsBroken { get { return shieldIsBroken; } set { shieldIsBroken = value; } }


    private List <IOnTakeDamage> OnTakeDamage = new List<IOnTakeDamage>();

    private List<IOnGameOver> OnGameOver = new List<IOnGameOver>();

    private bool isDead;
    public bool IsDead => isDead;



    public void RegisterListener(IOnGameOver listener)
    {
        OnGameOver.Add(listener);
    }
    public void RemoveListener(IOnGameOver listener)
    {
        OnGameOver.Remove(listener);
    }
    public void Invoke(bool isDead)
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


    private void Awake()
    {
        greenHouse = FindObjectOfType<Greenhouse>();
        h3Mine = FindObjectOfType<H3Mine>();
        researchLab = FindObjectOfType<ResearchLab>();
        mechanic = FindObjectOfType<Mechanic>();
        controls = GetComponent<Controls>();
        groundCheck = GetComponent<GroundCheck>();
    }
    private void Start()
    {
        isDead = false;
        health = maxHealth;
        shield = maxShield;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moveSpeed = rb.velocity.magnitude;
        GameOver();
        Repairing();
        if (Shield <= 0)
        {
            ShieldIsBroken = true;
        }
        else ShieldIsBroken = false;
        health = Mathf.Clamp(health, -1, maxHealth);
        shield = Mathf.Clamp(shield, 0, maxShield);
    }

    private void GameOver()
    {
        if (health <= 0)
        {
            Invoke(isDead);
        }
        if(greenHouse.IsDestroyed && h3Mine.IsDestroyed && researchLab.IsDestroyed && mechanic.IsDestroyed)
        {
            Invoke(isDead);
        }
    }
    private void Repairing()
    {
        if(controls.IsRepairing && groundCheck.IsOnMechanic && mechanic.IsDestroyed == false)
        {
            InvokeDamageTake(health / maxHealth);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Reduce bullet's damage from PlayerHP
        if (collision.gameObject.tag == "EnemyBullet" && shieldGO.activeInHierarchy == false)
        {
            EnemyBullet enemyBullet = collision.gameObject.GetComponent<EnemyBullet>();
            
            health -= enemyBullet.HPDAMAGE;
            audioSource2.Play();
            InvokeDamageTake(health / maxHealth);
            Destroy(enemyBullet.gameObject);
            if (health <= 0)
            {
                audioSource.Play();
                Invoke(isDead);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (moveSpeed <= 0.9f && moveSpeed >= 0.5f)
        {
            audioSource2.Play();
            health -= health * 0.5f;
            InvokeDamageTake(health / maxHealth);
        }
        else if (moveSpeed >= 1)
        {
            audioSource.Play();
            health = 0;
        }
    }
}
