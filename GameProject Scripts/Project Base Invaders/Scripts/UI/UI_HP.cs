using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour, IOnTakeDamage
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Image healthImage;

    private float hp;


    private void OnEnable()
    {
        playerHealth.RegisterDamageListener(this);
    }
    private void OnDisable()
    {
        playerHealth.RemoveDamageListener(this);
    }
    public void OnTakeDamage(float hp)
    {
        healthImage.fillAmount = hp;
    }
}
