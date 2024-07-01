using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class MothershipHeart : MonoBehaviour
{


    [SerializeField] private float health;

    [SerializeField] private bool isDead;

    [SerializeField] private GameObject floatingDamageNumber;

    [SerializeField] AudioSource creditsAudioSource;

    public bool IsDead => isDead;

    private List<IOnGameWin> OnGameWin = new List<IOnGameWin>();

    private bool hasWon;
    public void RegisterListener(IOnGameWin listener)
    {
        OnGameWin.Add(listener);
    }
    public void RemoveListener(IOnGameWin listener)
    {
        OnGameWin.Remove(listener);
    }
    public void Invoke(bool hasWon)
    {
        for (int i = 0; i < OnGameWin.Count; i++)
        {
            OnGameWin[i].OnGameWin(hasWon);
        }
    }
    private void Update()
    {
        if (health <= 0)
        {
            isDead = true;
            Invoke(hasWon);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            health -= bullet.HPDAMAGE;

            creditsAudioSource.Play();

            GameObject points = Instantiate(floatingDamageNumber, transform.position, Quaternion.identity) as GameObject;
            points.transform.GetChild(0).GetComponent<TextMeshPro>().text = bullet.HPDAMAGE.ToString();
        }
    }
}
