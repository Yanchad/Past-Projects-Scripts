using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleActiveCheck : MonoBehaviour
{
    [SerializeField] private GameObject module1;
    [SerializeField] private GameObject module2;
    [SerializeField] private GameObject module3;
    [SerializeField] private GameObject module4;

    [SerializeField] private bool noModulesLeft;
    [SerializeField] private bool module1IsAlive;
    [SerializeField] private bool module2IsAlive;
    [SerializeField] private bool module3IsAlive;
    [SerializeField] private bool module4IsAlive;

    public bool NoModulesLeft => noModulesLeft;
    public bool Module1IsAlive => module1IsAlive;
    public bool Module2IsAlive => module2IsAlive;
    public bool Module3IsAlive => module3IsAlive;
    public bool Module4IsAlive => module4IsAlive;
    

    private void Start()
    {
        module1IsAlive = true;
        module2IsAlive = true;
        module3IsAlive = true;
        module4IsAlive = true;
    }


    void Update()
    {
        ActiveCheck();
    }

    private void ActiveCheck()
    {
        if(!module1.activeInHierarchy) module1IsAlive = false;
        if(!module2.activeInHierarchy) module2IsAlive = false;
        if(!module3.activeInHierarchy) module3IsAlive = false;
        if(!module4.activeInHierarchy) module4IsAlive = false;

        if (!module1IsAlive && !module2IsAlive && !module3IsAlive && !module4IsAlive) noModulesLeft = true; else noModulesLeft = false;
    }
}
