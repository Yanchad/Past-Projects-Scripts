using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    Rigidbody2D rb;
    private SpriteRenderer sr;

    public bool isAlive;
    private bool isTakingDamage;

    private float interval;
    private float timer;
    private float damageInterval;
    [SerializeField] public int health;



    private void Awake()
    {
        isAlive = true;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        interval = 0.5f;
    }
    private void Update()
    {
        Death();
        DamageFlash();
        if (isTakingDamage)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                health--;
                timer = 0;
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBee")
        {
            health--;
            isTakingDamage = true;
        }
        if(health <= 0)
        {
            isAlive = false;
        }
        if(collision.gameObject.tag == "HP")
        {
            health += 1;
            Destroy(collision.gameObject);
        }
        

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBee")
        {
            isTakingDamage = false;
        }
    }


    private void Death()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
            Time.timeScale = 0; // pause upon dying
            isAlive = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) && isAlive == false)
        {
            gameObject.SetActive(true);
        }
    }


    private void DamageFlash()
    {
        if(isTakingDamage == true)
        {
            sr.color = Color.Lerp(Color.red, Color.white, Mathf.PingPong(Time.time * 10, 1));
        }
        else
        {
            sr.color = Color.white;
        }
    }
}
