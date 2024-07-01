using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDamageFlash : MonoBehaviour
{

    [SerializeField] private Sprite redTank;
    [SerializeField] private Sprite defaultTank;

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
                spriteRenderer.sprite = defaultTank;

                timer = damageFlashTime;
                return;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (gameObject.name == "Tank(Clone)")
            {
                spriteRenderer.sprite = redTank;
                damageSpriteActive = true;
                timer = damageFlashTime;
            }
        }
    }


}
