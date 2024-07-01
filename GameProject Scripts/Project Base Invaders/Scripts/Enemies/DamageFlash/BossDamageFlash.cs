using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageFlash : MonoBehaviour
{
    [SerializeField] private Sprite redBoss;
    [SerializeField] private Sprite defaultBoss;

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
                spriteRenderer.sprite = defaultBoss;

                timer = damageFlashTime;
                return;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (gameObject.name == "Boss1(Clone)")
            {
                spriteRenderer.sprite = redBoss;
                damageSpriteActive = true;
                timer = damageFlashTime;
            }
        }
    }





}
