using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class UI_FuelWarning : MonoBehaviour
{
    Fuel fuel;

    [SerializeField] private GameObject fuelLowGO;

    [SerializeField] private AudioSource audioSource;

    private float timer;

    void Start()
    {
        fuel = FindObjectOfType<Fuel>();
    }

    
    void Update()
    {

        if (fuel.Fuel1 <= 100)
        {
            fuelLowGO.SetActive(true);
        }
        else fuelLowGO.SetActive(false);

        if (fuelLowGO.activeSelf)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                audioSource.Play();
                timer = 1f;
            }
        }
    }
}
