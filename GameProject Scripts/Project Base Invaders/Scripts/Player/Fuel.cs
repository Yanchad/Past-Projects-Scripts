using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    Controls controls;
    GroundCheck groundCheck;
    Mechanic mechanic;

    [Tooltip("Maximum usable fuel")]
    [SerializeField] private float maxFuel = 100;
    [Tooltip("Current usable fuel")]
    [SerializeField] private float fuel;
    [Tooltip("Fuel consumption per second")]
    [SerializeField] private float fuelUsage = 10;

    private bool fuelEmpty;
    public bool FuelEmpty => fuelEmpty;
    public float Fuel1 { get { return fuel; } set { fuel = value; } }
    public float MaxFuel { get { return maxFuel; } set { maxFuel = value; } }

    private List<IOnFuelChanged> OnFuelChanged = new List<IOnFuelChanged>();



    public void RegisterListener(IOnFuelChanged listener)
    {
        OnFuelChanged.Add(listener);
    }
    public void RemoveListener(IOnFuelChanged listener)
    {
        OnFuelChanged.Remove(listener);
    }
    public void Invoke(float fuel)
    {
        for(int i = 0; i < OnFuelChanged.Count; i++)
        {
            OnFuelChanged[i].OnFuelChanged(fuel);
        }
    }
    private void Awake()
    {
        controls = GetComponent<Controls>();
        groundCheck = GetComponent<GroundCheck>();
        mechanic = FindObjectOfType<Mechanic>();
    }
    void Start()
    {
        fuel = maxFuel;
    }

    
    void Update()
    {
        if(fuelEmpty == false && controls.IsThrusting == true)
        {
            fuel -= fuelUsage * Time.deltaTime;
            fuel = Mathf.Clamp(fuel, 0, maxFuel);
            Invoke(fuel / maxFuel);
            if (fuel <= 0)
            {
                fuelEmpty = true;
            }
        }
        else if (fuel >= 1)
        {
            fuelEmpty = false;
        }
        Refuel();
    }
    private void Refuel()
    {
        if(controls.IsRefueling && groundCheck.IsOnMechanic && mechanic.IsDestroyed == false)
        {
            fuel = maxFuel;
            Invoke(fuel / maxFuel);
        }
    }
}
