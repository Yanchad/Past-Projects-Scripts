using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPdamageFlash : MonoBehaviour, IOnTakeDamage
{
    PlayerHealth playerHealth;

    [SerializeField] private Sprite redPlayer;
    [SerializeField] private Sprite defaultPlayer;

    private bool damageSpriteActive;

    [SerializeField] private float timer;
    [SerializeField] private float damageFlashTime;

    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        playerHealth.RegisterDamageListener(this);
    }
    private void OnDisable()
    {
        playerHealth.RemoveDamageListener(this);
    }
    public void OnTakeDamage(float hp)
    {
        spriteRenderer.sprite = redPlayer;
        damageSpriteActive = true;
        timer = damageFlashTime;
    }


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageSpriteActive = true;
    }
    
    void Update()
    {
        if (damageSpriteActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                spriteRenderer.sprite = defaultPlayer;

                timer = damageFlashTime;
                return;
            }
        }
    }
}
