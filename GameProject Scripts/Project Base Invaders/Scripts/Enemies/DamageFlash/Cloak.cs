using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Cloak : MonoBehaviour
{
    [SerializeField] private float setTimeToCloak;
    [SerializeField] private float setCloakActiveTime;

    private float timeToCloak;
    private float cloakActiveTime;


    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite defaultCloak;
    [SerializeField] private Sprite cloakedSprite;

    [SerializeField] private Sprite redCloak;

    private PolygonCollider2D polygonCollider;
    private bool damageSpriteActive;

    [SerializeField] private float timer;
    [SerializeField] private float damageFlashTime;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        damageSpriteActive = true;
    }

    
    void Update()
    {
        timeToCloak -= Time.deltaTime;

        if(timeToCloak <= 0)
        {
            spriteRenderer.sprite = cloakedSprite;
            cloakActiveTime -= Time.deltaTime;
            polygonCollider.enabled = false;
            damageSpriteActive = false;
            if(cloakActiveTime <= 0)
            {
                spriteRenderer.sprite = defaultCloak;
                timeToCloak = setTimeToCloak;
                cloakActiveTime = setCloakActiveTime;
                damageSpriteActive = true;
                polygonCollider.enabled = true;
                return;
            }
        }
        if (damageSpriteActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                spriteRenderer.sprite = defaultCloak;

                timer = damageFlashTime;
                return;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (gameObject.name == "Stealth(Clone)")
            {
                spriteRenderer.sprite = redCloak;
                damageSpriteActive = true;
                timer = damageFlashTime;
            }
        }
    }
}
