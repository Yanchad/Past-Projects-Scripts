using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleSwitch : MonoBehaviour
{
    Greenhouse greenHouse;
    H3Mine h3mine;
    ResearchLab researchLab;
    Mechanic mechanic;

    [SerializeField] private GameObject greenHouseGO;
    [SerializeField] private GameObject h3MineGO;
    [SerializeField] private GameObject researchLabGO;
    [SerializeField] private GameObject mechanicGO;

    [SerializeField] private GameObject rubbleGreenhouse;
    [SerializeField] private GameObject rubbleH3Mine;
    [SerializeField] private GameObject rubbleResearch;
    [SerializeField] private GameObject rubbleMechanic;

    void Awake()
    {
        greenHouse = GetComponentInChildren<Greenhouse>();
        h3mine = GetComponentInChildren<H3Mine>();
        researchLab = GetComponentInChildren<ResearchLab>();
        mechanic = GetComponentInChildren<Mechanic>();
    }

    
    void Update()
    {
        ActivateRubbleGreenHouse();
        ActivateRubbleH3Mine();
        ActivateRubbleResearchLab();
        ActivateRubbleMechanic();
    }
    private void ActivateRubbleGreenHouse()
    {
        if (greenHouse.IsDestroyed == true)
        {
            rubbleGreenhouse.SetActive(true);
        }
        else
        {
            rubbleGreenhouse.SetActive(false);
            greenHouseGO.SetActive(true);
        }
    }
    private void ActivateRubbleH3Mine()
    {
        if (h3mine.IsDestroyed == true)
        {
            rubbleH3Mine.SetActive(true);
        }
        else
        {
            rubbleH3Mine.SetActive(false);
            h3MineGO.SetActive(true);
        }
    }
    private void ActivateRubbleResearchLab()
    {
        if (researchLab.IsDestroyed == true)
        {
            rubbleResearch.SetActive(true);
        }
        else
        {
            rubbleResearch.SetActive(false);
            researchLabGO.SetActive(true);
        }
    }
    private void ActivateRubbleMechanic()
    {
        if (mechanic.IsDestroyed == true)
        {
            rubbleMechanic.SetActive(true);
        }
        else
        {
            rubbleMechanic.SetActive(false);
            mechanicGO.SetActive(true);
        }
    }

}
