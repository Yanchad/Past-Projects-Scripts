using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_doorLockHints : MonoBehaviour
{
    [SerializeField] private GameObject hint1Text;
    [SerializeField] private GameObject hint2Text;
    [SerializeField] private GameObject hint3Text;
    [SerializeField] private GameObject hint4Text;


    ActivateUpgrade activateUpgrade;

    private void Start()
    {
        activateUpgrade = FindObjectOfType<ActivateUpgrade>();
    }

    private void Update()
    {
        if (activateUpgrade.hasHint1)
        {
            hint1Text.SetActive(true);
        }
        if (activateUpgrade.hasHint2)
        {
            hint2Text.SetActive(true);
        }
        if (activateUpgrade.hasHint3)
        {
            hint3Text.SetActive(true);
        }
        if (activateUpgrade.hasHint4)
        {
            hint4Text.SetActive(true);
        }
    }
}
