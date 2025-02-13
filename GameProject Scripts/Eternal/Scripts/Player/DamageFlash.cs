using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour, IOnTakeDamage, IOnHealHp
{

    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float flashTime;
    [SerializeField] private PlayerHealth playerHealth;



    private float timer;

    private bool damageColorActive;
    private bool healColorActive;


    private void OnEnable()
    {
        playerHealth.RegisterDamageListener(this);
        playerHealth.RegisterHealListener(this);
    }
    private void OnDisable()
    {
        playerHealth.RemoveDamageListener(this);
        playerHealth.RemoveHealListener(this);
    }

    public void OnTakeDamage(float hp)
    {
        playerSprite.color = Color.red;
        damageColorActive = true;
    }


    private void Update()
    {
        if (damageColorActive)
        {
            timer += Time.deltaTime;

            if(timer >= flashTime)
            {
                playerSprite.color = Color.white;

                timer = 0f;
                return;
            }
        }
        else if (healColorActive)
        {
            timer += Time.deltaTime;

            if (timer >= flashTime)
            {
                playerSprite.color = Color.white;

                timer = 0f;
                return;
            }
        }
    }

    public void OnHealHp(float hp)
    {
        playerSprite.color = Color.green;
        healColorActive = true;
    }
}
