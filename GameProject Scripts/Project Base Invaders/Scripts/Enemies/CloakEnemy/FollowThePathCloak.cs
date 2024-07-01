using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePathCloak : MonoBehaviour
{
    Alarm alarm;
    WaveSpawner waveSpawner;
    MotherShipWaypoints mothershipWayPoints;

    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private int wayPointIndex = 0;

    public Transform[] WayPoints { get { return wayPoints; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public int WayPointIndex => wayPointIndex;

    private Transform wayPointParentCloak1;
    private Transform wayPointParentCloak2;
    private Transform wayPointParentCloak3;
    private Transform wayPointParentCloak4;
    private Transform waypointParentForLost;



    void Awake()
    {
        alarm = FindObjectOfType<Alarm>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        mothershipWayPoints = GetComponent<MotherShipWaypoints>();

        waypointParentForLost = GameObject.Find("WaypointParentForLost").transform;

        wayPointParentCloak1 = GameObject.Find("WaypointParentCloak1").transform;
        wayPointParentCloak2 = GameObject.Find("WaypointParentCloak2").transform;
        wayPointParentCloak3 = GameObject.Find("WaypointParentCloak3").transform;
        wayPointParentCloak4 = GameObject.Find("WaypointParentCloak4").transform;
    }
    void Start()
    {
        chooseParent(waypointParentForLost);
        if (alarm.greenhouseAlarm.activeInHierarchy)
        {
            chooseParent(wayPointParentCloak1);
        }

        if (alarm.h3MineAlarm.activeInHierarchy)
        {
            chooseParent(wayPointParentCloak2);
        }

        if (alarm.researchLabAlarm.activeInHierarchy)
        {
            chooseParent(wayPointParentCloak3);
        }

        if (alarm.mechanicAlarm.activeInHierarchy)
        {
            chooseParent(wayPointParentCloak4);
        }
        if (waveSpawner.CurrentWaveIndex >= waveSpawner.waves.Length - 1)
        {
            chooseParent(mothershipWayPoints.MothershipRandomWaypoint);
        }
        transform.position = wayPoints[wayPointIndex].transform.position;
    }
    private void Update()
    {
        Move();
    }


    private void Move()
    {
        if (wayPointIndex <= wayPoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex].transform.position, moveSpeed * Time.deltaTime);
        }

        if (transform.position == wayPoints[wayPointIndex].transform.position)
        {
            wayPointIndex += 1;
        }
    }
    private void chooseParent(Transform patrolPointParent)
    {
        wayPoints = new Transform[patrolPointParent.childCount];
        for (int i = 0; i < patrolPointParent.childCount; i++)
        {
            wayPoints[i] = patrolPointParent.GetChild(i).transform;
        }
    }
}
