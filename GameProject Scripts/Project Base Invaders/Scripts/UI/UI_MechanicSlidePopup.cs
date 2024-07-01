using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_MechanicSlidePopup : MonoBehaviour
{
    GroundCheck groundCheck;
    PlayerHealth health;
    Controls controls;
    Score score;
    Mechanic mechanic;
    Fuel fuel;

    [SerializeField] Animator mechanicPopUpanimator;

    [SerializeField] private GameObject mechanicPopup;
    [SerializeField] private TextMeshProUGUI repairTxt;
    [SerializeField] private TextMeshProUGUI repairCostTxt;
    [SerializeField] private TextMeshProUGUI refuelTxt;
    [SerializeField] private TextMeshProUGUI refuelCostTxt;


    private float maxAll;
    private float currentAll;

    private float repairCostAll;
    private float repairCost;
    private float refuelCostAll;
    private float refuelCost;

    void Start()
    {
        health = FindObjectOfType<PlayerHealth>();
        groundCheck = FindObjectOfType<GroundCheck>();
        controls = FindObjectOfType<Controls>();
        score = FindObjectOfType<Score>();
        mechanic = FindObjectOfType<Mechanic>();
        fuel = FindObjectOfType<Fuel>();
    }

    
    void Update()
    {
        repairCostTxt.text = "(" + repairCost + " C)";
        refuelCostTxt.text = "(" + refuelCost.ToString("0") + " C)";

        UI_Slide();

        if (groundCheck.IsOnMechanic && (health.Health < health.MaxHealth ||  health.Shield < health.MaxShield) && mechanic.IsDestroyed == false && score.Currency > repairCost)
        {
            repairTxt.color = Color.white;
            
        }else repairTxt.color = Color.grey;

        if (groundCheck.IsOnMechanic && fuel.Fuel1 < fuel.MaxFuel && mechanic.IsDestroyed == false)
        {
            refuelTxt.color = Color.white;
        }
        else refuelTxt.color = Color.grey;

        CalculatePriceRepair();
        CalculatePriceRefuel();
        BuyRepair();
        BuyFuel();
    }
    private void UI_Slide()
    {
        if (groundCheck.IsOnMechanic && mechanic.IsDestroyed == false)
        {
            mechanicPopUpanimator.enabled = true;
            mechanicPopUpanimator.SetBool("SlideOn", true);
            mechanicPopUpanimator.SetBool("slideOff1", false);
        }
        else
        {
            mechanicPopUpanimator.SetBool("SlideOn", false);
            mechanicPopUpanimator.SetBool("slideOff1", true);
        }
            
            
    }

    private void BuyRepair()
    {
        if (controls.IsRepairing)
        {
            if(score.Currency >= repairCost)
            {
                score.Currency -= repairCost;
                health.Health = health.MaxHealth;
                health.Shield = health.MaxShield;
            }
        }
    }
    private void BuyFuel()
    {
        if (controls.IsRefueling)
        {
            score.Currency -= refuelCost;
            fuel.Fuel1 = fuel.MaxFuel;
        }
    }


    private void CalculatePriceRepair()
    {
        maxAll = health.MaxHealth + health.MaxShield;
        currentAll = health.Health + health.Shield;

        repairCostAll = maxAll - currentAll;

        repairCost = repairCostAll * 2;
    }
    private void CalculatePriceRefuel()
    {
        refuelCostAll = fuel.MaxFuel - fuel.Fuel1;
        refuelCost = refuelCostAll / 5;
    }
}
