using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryDamageFlash : MonoBehaviour
{
    [SerializeField] private Sprite redArtillery;
    [SerializeField] private Sprite defaultArtillery;

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
                spriteRenderer.sprite = defaultArtillery;

                timer = damageFlashTime;
                return;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (gameObject.name == "Artillery(Clone)")
            {
                spriteRenderer.sprite = redArtillery;
                damageSpriteActive = true;
                timer = damageFlashTime;
            }
        }
    }
}
