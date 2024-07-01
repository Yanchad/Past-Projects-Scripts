using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDamageFlash : MonoBehaviour
{
    [SerializeField] private Sprite redBasic;
    [SerializeField] private Sprite defaultBasic;

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
                spriteRenderer.sprite = defaultBasic;

                timer = damageFlashTime;
                return;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (gameObject.name == "Basic(Clone)")
            {
                spriteRenderer.sprite = redBasic;
                damageSpriteActive = true;
                timer = damageFlashTime;
            }
        }
    }
}
