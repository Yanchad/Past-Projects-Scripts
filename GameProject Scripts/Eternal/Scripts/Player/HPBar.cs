using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour, IOnTakeDamage, IOnHealHp
{
    [SerializeField] private GameObject hpBarGO;
    [SerializeField] private Image hpBar;
    [SerializeField] private PlayerHealth playerHealth;
    private void Start()
    {
        hpBarGO.SetActive(false);
    }
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
        if (!hpBarGO.activeInHierarchy) hpBarGO.SetActive(true);
        if(hpBar != null) hpBar.fillAmount = hp;
    }

    public void OnHealHp(float hp)
    {
        if (hpBar != null) hpBar.fillAmount = hp;
        if(hpBarGO.activeInHierarchy && hp == playerHealth.MaxHealth)
        {
            hpBarGO.SetActive(false);
        }
    }
}
