using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDamageFlash : MonoBehaviour
{
    [SerializeField] private Sprite redHeart;
    [SerializeField] private Sprite defaultHeart;

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
                spriteRenderer.sprite = defaultHeart;

                timer = damageFlashTime;
                return;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            spriteRenderer.sprite = redHeart;
            damageSpriteActive = true;
            timer = damageFlashTime;
        }
    }
}
