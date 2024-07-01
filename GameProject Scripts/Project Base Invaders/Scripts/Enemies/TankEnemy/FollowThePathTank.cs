using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePathTank : MonoBehaviour
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

    private Transform wayPointParentTank1;
    private Transform wayPointParentTank2;
    private Transform wayPointParentTank3;
    private Transform wayPointParentTank4;
    private Transform waypointParentForLost;



    void Awake()
    {
        alarm = FindObjectOfType<Alarm>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        mothershipWayPoints = GetComponent<MotherShipWaypoints>();

        wayPointParentTank1 = GameObject.Find("WaypointParentTank1").transform;
        wayPointParentTank2 = GameObject.Find("WaypointParentTank2").transform;
        wayPointParentTank3 = GameObject.Find("WaypointParentTank3").transform;
        wayPointParentTank4 = GameObject.Find("WaypointParentTank4").transform;

        waypointParentForLost = GameObject.Find("WaypointParentForLost").transform;
    }
    void Start()
    {
        chooseParent(waypointParentForLost);
        if (alarm.greenhouseAlarm.activeInHierarchy)
        {
            chooseParent(wayPointParentTank1);
        }

        if (alarm.h3MineAlarm.activeInHierarchy)
        {
            chooseParent(wayPointParentTank2);
        }

        if (alarm.researchLabAlarm.activeInHierarchy)
        {
            chooseParent(wayPointParentTank3);
        }

        if (alarm.mechanicAlarm.activeInHierarchy)
        {
            chooseParent(wayPointParentTank4);
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
