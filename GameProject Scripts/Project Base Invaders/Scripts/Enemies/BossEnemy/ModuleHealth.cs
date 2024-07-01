using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModuleHealth : MonoBehaviour
{

    [SerializeField] private float moduleHealth;

    [SerializeField] private GameObject floatingDamageNumber;

    [SerializeField] AudioSource creditsAudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            moduleHealth -= bullet.HPDAMAGE;

            creditsAudioSource.Play();

            GameObject points = Instantiate(floatingDamageNumber, transform.position, Quaternion.identity) as GameObject;
            points.transform.GetChild(0).GetComponent<TextMeshPro>().text = bullet.HPDAMAGE.ToString();

            if (moduleHealth <= 0)
            {
                gameObject.SetActive(false);
            }
            else gameObject.SetActive(true);
        }
    }
}
