using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipWaypoints : MonoBehaviour
{
    WaveSpawner waveSpawner;
    ModuleActiveCheck moduleActiveCheck;

    [SerializeField] private List<GameObject> motherShipModules = new List<GameObject>();

    private GameObject module1;
    private GameObject module2;
    private GameObject module3;
    private GameObject module4;

    [SerializeField] private List<Transform> mothershipWayPointParents = new List<Transform>();

    private Transform waypointParentMotherShip1;
    private Transform waypointParentMotherShip2;
    private Transform waypointParentMotherShip3;
    private Transform waypointParentMotherShip4;
    private int mothershipWaypointIndex;

    private Transform mothershipRandomWaypoint;
    public Transform MothershipRandomWaypoint => mothershipRandomWaypoint;


    void Awake()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();

        if (waveSpawner.CurrentWaveIndex >= waveSpawner.waves.Length - 1) ModuleList();
        if (waveSpawner.CurrentWaveIndex >= waveSpawner.waves.Length - 1) MotherShipWaypointList();
    }

    private void ModuleList()
    {
        moduleActiveCheck = FindObjectOfType<ModuleActiveCheck>();
        module1 = GameObject.Find("SR_Module1");
        module2 = GameObject.Find("SR_Module2");
        module3 = GameObject.Find("SR_Module3");
        module4 = GameObject.Find("SR_Module4");

        if (moduleActiveCheck.Module1IsAlive)
        {
            motherShipModules.Add(module1);
        }

        if (moduleActiveCheck.Module2IsAlive)
        {
            motherShipModules.Add(module2);
        }

        if (moduleActiveCheck.Module3IsAlive)
        {
            motherShipModules.Add(module3);
        }

        if (moduleActiveCheck.Module4IsAlive)
        {
            motherShipModules.Add(module4);
        }
    }
    private void MotherShipWaypointList()
    {
        waypointParentMotherShip1 = GameObject.Find("WaypointParentMotherShip1").transform;
        waypointParentMotherShip2 = GameObject.Find("WaypointParentMotherShip2").transform;
        waypointParentMotherShip3 = GameObject.Find("WaypointParentMotherShip3").transform;
        waypointParentMotherShip4 = GameObject.Find("WaypointParentMotherShip4").transform;

        if (motherShipModules.Contains(module1))
        {
            mothershipWayPointParents.Add(waypointParentMotherShip1);
        }

        if (motherShipModules.Contains(module2))
        {
            mothershipWayPointParents.Add(waypointParentMotherShip2);
        }

        if (motherShipModules.Contains(module3))
        {
            mothershipWayPointParents.Add(waypointParentMotherShip3);
        }

        if (motherShipModules.Contains(module4))
        {
            mothershipWayPointParents.Add(waypointParentMotherShip4);
        }

        mothershipWaypointIndex = Random.Range(0, mothershipWayPointParents.Count);
        mothershipRandomWaypoint = mothershipWayPointParents[mothershipWaypointIndex];
    }
}
