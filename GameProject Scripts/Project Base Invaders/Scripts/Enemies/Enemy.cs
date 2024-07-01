using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    WaveSpawner waveSpawner;
    Score score;

    [SerializeField] private float health;
    [SerializeField] private float shield;

    [SerializeField] private float damageToBase;
    [SerializeField] private float creditGains;
    [SerializeField] private float scoreGains;
    [SerializeField] private GameObject floatingDamageNumber;
    [SerializeField] private GameObject floatingScoreNumber;

    [SerializeField] AudioSource hitAudioSource;

    public float Health => health;
    private bool shieldIsBroken;

    public float DamageToBase { get { return damageToBase; }set { damageToBase = value; } }



    private void Start()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        score = FindObjectOfType<Score>();
    }

    void Update()
    {
        if (shield <= 0)
        {
            shieldIsBroken = true;

        }
        else shieldIsBroken = false;

        shield = Mathf.Clamp(shield, 0, 1000);
        health = Mathf.Clamp(health, 0, 1000);
        damageToBase = Mathf.Clamp(damageToBase, 0, 1000);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Reduce the bullet's damage from Shield
        if (collision.gameObject.tag == "Bullet" && shieldIsBroken == false)
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            shield -= bullet.SHIELDDAMAGE;

        }
        //Reduce the bullet's damage from Health
        if (collision.gameObject.tag == "Bullet")
        {

            if (shieldIsBroken)
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();

                health -= bullet.HPDAMAGE;

                hitAudioSource.Play();

                GameObject points = Instantiate(floatingDamageNumber, transform.position, Quaternion.identity) as GameObject;
                points.transform.GetChild(0).GetComponent<TextMeshPro>().text = bullet.HPDAMAGE.ToString();
                
                if (health <= 0)
                {
                    GameObject points1 = Instantiate(floatingScoreNumber, transform.position, Quaternion.identity) as GameObject;
                    points1.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+" + creditGains.ToString();
                    Destroy(gameObject);
                    score.PlayerScore += scoreGains;
                    score.Currency += creditGains;
                }
            }
        }
    }




}
