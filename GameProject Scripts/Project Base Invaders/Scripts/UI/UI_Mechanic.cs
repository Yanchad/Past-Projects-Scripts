
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Mechanic : MonoBehaviour
{
    PlayerHealth playerHealth;
    PlayerController playerController;
    Score score;
    Abilities abilities;
    MechanicPopup mechanicPopup;
    
    [SerializeField] Bullet bullet;

    [Header("HoverPanels")]
    [SerializeField] private GameObject hpHoverPanel;
    [SerializeField] private GameObject shieldHoverPanel;
    [SerializeField] private GameObject hpDamageHoverPanel;
    [SerializeField] private GameObject thrusterHoverPanel;
    [SerializeField] private GameObject emergencyThrusterHoverPanel;
    [Header("HoverPanelCostTexts")]
    [SerializeField] private TextMeshProUGUI hpHoverCostTxt;
    [SerializeField] private TextMeshProUGUI shieldHoverCostTxt;
    [SerializeField] private TextMeshProUGUI damageHoverCostTxt;
    [SerializeField] private TextMeshProUGUI thrusterHoverCostTxt;
    [SerializeField] private TextMeshProUGUI emergencyThrusterCostTxt;
    [Header("Buttons")]
    [SerializeField] private Button hpPlusBtn;
    [SerializeField] private Button shieldPlusBtn;
    [SerializeField] private Button damagePlusBtn;
    [SerializeField] private Button thrusterPlusBtn;
    [SerializeField] private Button emergencyThrusterPlusBtn;
    [Header("Amount Texts")]
    [SerializeField] private TextMeshProUGUI hpAmountTxt;
    [SerializeField] private TextMeshProUGUI shieldAmountText;
    [SerializeField] private TextMeshProUGUI damageAmountTxt;
    [SerializeField] private TextMeshProUGUI thrusterAmountText;
    [Header("Buy Amounts")]
    [SerializeField] private float buyAmountHP = 5;
    [SerializeField] private float buyAmountShield = 5;
    [SerializeField] private float buyAmountHPDamage = 1;
    [SerializeField] private float buyAmountThruster = 1;
    [Header("Prices")]
    [SerializeField] private float hpCost;
    [SerializeField] private float shieldCost;
    [SerializeField] private float damageCost;
    [SerializeField] private float thrusterCost;
    [SerializeField] private float emergencyThrusterCost;
    [Header("Ability Sprites")]
    [SerializeField] private Sprite ETColored;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;


    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerController = FindObjectOfType<PlayerController>();
        score = FindObjectOfType<Score>();
        abilities = FindObjectOfType<Abilities>();
        mechanicPopup = GetComponentInParent<MechanicPopup>();
    }


    private void Update()
    {
        hpAmountTxt.text = playerHealth.MaxHealth.ToString();
        if(score.Currency < hpCost)
        {
            hpPlusBtn.image.color = Color.grey;
        }else hpPlusBtn.image.color = Color.white;


        shieldAmountText.text = playerHealth.MaxShield.ToString();
        if(score.Currency < shieldCost)
        {
            shieldPlusBtn.image.color = Color.grey;
        }else shieldPlusBtn.image.color = Color.white;


        damageAmountTxt.text = bullet.HPDAMAGE.ToString();
        if(score.Currency < damageCost)
        {
            damagePlusBtn.image.color = Color.grey;
        }else damagePlusBtn.image.color = Color.white;

        thrusterAmountText.text = (playerController.ThrustForce * 10).ToString("0");
        if(score.Currency < thrusterCost)
        {
            thrusterPlusBtn.image.color = Color.grey;
        }else thrusterPlusBtn.image.color = Color.white;

        if (score.Currency < emergencyThrusterCost && abilities.HasEmergencyThrusters == false)
        {
            emergencyThrusterPlusBtn.image.sprite = ETColored;
            emergencyThrusterPlusBtn.image.color = Color.red;

        } else if (score.Currency >= emergencyThrusterCost && abilities.HasEmergencyThrusters == false) 
        {
            emergencyThrusterPlusBtn.image.color = Color.white;
        } 
        else if(abilities.HasEmergencyThrusters == true)
        {
            emergencyThrusterPlusBtn.image.sprite = ETColored;
            emergencyThrusterPlusBtn.image.color = Color.grey;
        }
        HoverPanelCostText();
    }

    //BUY METHODS
    public void BuyHP()
    {
        if(score.Currency >= hpCost)
        {
            audioSource2.Play();
            playerHealth.MaxHealth += buyAmountHP;
            playerHealth.Health += buyAmountHP;
            score.Currency -= hpCost;
            hpCost += 10;
        }
    }
    public void BuyShield()
    {
        if(score.Currency >= shieldCost)
        {
            audioSource2.Play();
            playerHealth.MaxShield += buyAmountShield;
            playerHealth.Shield += buyAmountShield;
            score.Currency -= shieldCost;
            shieldCost += 25;
        }
    }
    public void BuyHPDamage()
    {
        if(score.Currency >= damageCost)
        {
            audioSource2.Play();
            bullet.HPDAMAGE += buyAmountHPDamage;
            score.Currency -= damageCost;
            damageCost += 10f;
        }
    }
    public void BuyThrusterForce()
    {
        if(score.Currency >= thrusterCost)
        {
            audioSource2.Play();
            playerController.ThrustForce += buyAmountThruster;
            score.Currency -= thrusterCost;
            thrusterCost += 10;
        }
    }
    public void BuyEmergencyThruster()
    {
        if (score.Currency >= emergencyThrusterCost && abilities.HasEmergencyThrusters == false)
        {
            audioSource2.Play();
            score.Currency -= emergencyThrusterCost;
            abilities.HasEmergencyThrusters = true;
            abilities.Timer = 0;
        }
        else abilities.HasEmergencyThrusters = false;
    }

    //HOVERPANEL ACTIVATIONS
    public void HoverHP()
    {
        audioSource1.Play();
        hpHoverPanel.SetActive(true);
    }
    public void UnHoverHp()
    {
        hpHoverPanel.SetActive(false);
    }
    public void HoverShield()
    {
        audioSource1.Play();
        shieldHoverPanel.SetActive(true);
    }
    public void UnHoverShield()
    {
        shieldHoverPanel.SetActive(false);
    }
    public void HoverDamage()
    {
        audioSource1.Play();
        hpDamageHoverPanel.SetActive(true);
    }
    public void UnHoverDamage()
    {
        hpDamageHoverPanel.SetActive(false);
    }
    public void HoverThruster()
    {
        audioSource1.Play();
        thrusterHoverPanel.SetActive(true);
    }
    public void UnHoverThruster()
    {
        thrusterHoverPanel.SetActive(false);
    }
    public void HoverEmergencyThruster()
    {
        audioSource1.Play();
        emergencyThrusterHoverPanel.SetActive(true);
    }
    public void UnHoverEmergencyThruster()
    {
        emergencyThrusterHoverPanel.SetActive(false);
    }
    public void HoverCloseBtn()
    {
        audioSource1.Play();
    }
    //HOVER PANEL COST TEXT
    private void HoverPanelCostText()
    {
        hpHoverCostTxt.text = "Cost: " + hpCost + " C";
        shieldHoverCostTxt.text = "Cost: " + shieldCost + " C";
        damageHoverCostTxt.text = "Cost: " + damageCost + " C";
        thrusterHoverCostTxt.text = "Cost: " + thrusterCost + " C";
        emergencyThrusterCostTxt.text = "Cost: " + emergencyThrusterCost + " C";
    }

    //CLOSE BUTTON ACTION
    public void CloseWindow()
    {
        mechanicPopup.Hide = true;

    }
    public void CloseWindowClickAudio()
    {
        audioSource2.Play();
    }
}
