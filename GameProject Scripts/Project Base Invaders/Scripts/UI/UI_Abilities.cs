using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Abilities : MonoBehaviour
{
    Abilities abilities;
    PlayerHealth playerHealth;

    [Header("Ablity Icon Images")]
    [SerializeField] private Image emergencyThrusterImage;
    [SerializeField] private Image shieldImage;

    [Header("Ability Image's Sprites")]
    [SerializeField] private Sprite ETcolored;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI cooldownTimerTxt;
    

    void Start()
    {
        abilities = FindObjectOfType<Abilities>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    
    void Update()
    {
        EmergencyAbilityIcon();
        EmergencyAbilityCooldown();
        ShieldAbilityIcon();
    }

    private void EmergencyAbilityIcon()
    {
        if(abilities.HasEmergencyThrusters == false)
        {
            emergencyThrusterImage.sprite = ETcolored;
            emergencyThrusterImage.color = Color.grey;
        }
        else if(abilities.HasEmergencyThrusters == true && abilities.CanUse == true)
        {
            emergencyThrusterImage.sprite = ETcolored;
            emergencyThrusterImage.color = Color.white;
        }
        else if(abilities.HasEmergencyThrusters == true && abilities.CanUse == false)
        {
            emergencyThrusterImage.sprite = ETcolored;
            emergencyThrusterImage.color = Color.grey;
        }
    }
    private void EmergencyAbilityCooldown()
    {
        if(abilities.CanUse == false && abilities.HasEmergencyThrusters == true)
        {
            cooldownTimerTxt.enabled = true;
            cooldownTimerTxt.text = abilities.Timer.ToString("0");
        }else cooldownTimerTxt.enabled = false;
    }
    private void ShieldAbilityIcon()
    {
        if(playerHealth.ShieldIsBroken == false)
        {
            shieldImage.color = Color.white;
        }else shieldImage.color = Color.grey;
    }


}
