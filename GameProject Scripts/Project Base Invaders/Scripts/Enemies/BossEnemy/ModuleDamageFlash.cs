using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDamageFlash : MonoBehaviour
{

    [SerializeField] private Sprite redModule;
    [SerializeField] private Sprite defaultModule;

    private bool damageSpriteActive;

    [SerializeField] private float timer;
    [SerializeField] private float damageFlashTime;

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        damageSpriteActive = true;
    }


    void Update()
    {
        if (damageSpriteActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                spriteRenderer.sprite = defaultModule;

                timer = damageFlashTime;
                return;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            spriteRenderer.sprite = redModule;
            damageSpriteActive = true;
            timer = damageFlashTime;
        }
    }
}
