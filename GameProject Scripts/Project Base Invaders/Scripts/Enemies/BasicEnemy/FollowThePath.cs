using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class FollowThePath : MonoBehaviour
{
    Alarm alarm;
    WaveSpawner waveSpawner;
    MotherShipWaypoints mothershipWayPoints;

    [SerializeField] private Transform[] waypoints;
    

    [SerializeField] private float moveSpeed = 1f;
    
    private int waypointIndex = 0;
    
    public Transform[] Waypoints { get { return waypoints; } }
    public int WaypointIndex => waypointIndex;

    private Transform waypointParent1;
    private Transform waypointParent2;
    private Transform waypointParent3;
    private Transform waypointParent4;
    private Transform waypointParentForLost;


    private void Awake()
    {
        alarm = FindObjectOfType<Alarm>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        mothershipWayPoints = GetComponent<MotherShipWaypoints>();

        waypointParent1 = GameObject.Find("WaypointParent1").transform;
        waypointParent2 = GameObject.Find("WaypointParent2").transform;
        waypointParent3 = GameObject.Find("WaypointParent3").transform;
        waypointParent4 = GameObject.Find("WaypointParent4").transform;
        waypointParentForLost = GameObject.Find("WaypointParentForLost").transform;

    }

    void Start()
    {
        chooseParent(waypointParentForLost);
        if(alarm.greenhouseAlarm.activeInHierarchy) 
        {
            chooseParent(waypointParent1);
        } 

        if (alarm.h3MineAlarm.activeInHierarchy)
        {
            chooseParent(waypointParent2);
        }

        if (alarm.researchLabAlarm.activeInHierarchy)
        {
            chooseParent(waypointParent3);
        }

        if (alarm.mechanicAlarm.activeInHierarchy)
        {
            chooseParent(waypointParent4);
        }
        if (waveSpawner.CurrentWaveIndex >= waveSpawner.waves.Length - 1)
        {
            chooseParent(mothershipWayPoints.MothershipRandomWaypoint);
        }
        transform.position = waypoints[waypointIndex].transform.position;
    }
    void Update()
    {
        Move();
    }


    private void Move()
    {
        
        if(waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
        }

        if(transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }
    }
    private void chooseParent(Transform waypointParent)
    {
        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i).transform;
        }
    }
}
