using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlashEnemy : MonoBehaviour, IOnEnemyTakeDamage
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float flashTime;

    [SerializeField] private EnemyHealth enemyHealth;

    private float timer;

    private bool damageColorActive;


    private void OnEnable()
    {
        enemyHealth.RegisterDamageListener(this);
    }
    private void OnDisable()
    {
        enemyHealth.RemoveDamageListener(this);
    }
    public void OnTakeDamageEnemy(float hp)
    {
        playerSprite.color = Color.white;
        damageColorActive = true;
    }
    private void Update()
    {
        if (damageColorActive)
        {
            timer += Time.deltaTime;

            if (timer >= flashTime)
            {
                playerSprite.color = Color.red;

                timer = 0f;
                return;
            }
        }
    }


}
