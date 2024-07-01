using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTheSkyBoss : MonoBehaviour
{
    Alarm alarm;

    [SerializeField] private Transform[] patrolPoints;
    public Transform[] PatrolPoints { get { return patrolPoints; } }

    [SerializeField] private float moveSpeed = 1f;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    [SerializeField] private int patrolPointIndex = 0;
    public int PatrolPointIndex => patrolPointIndex;

    private Transform bossPointParent;


    private void Awake()
    {
        alarm = FindObjectOfType<Alarm>();
    }
    void Start()
    {
        bossPointParent = GameObject.Find("BossPointParent").transform;


        if (alarm.greenhouseAlarm.activeInHierarchy || alarm.h3MineAlarm.activeInHierarchy || alarm.researchLabAlarm.activeInHierarchy || alarm.mechanicAlarm.activeInHierarchy) chooseParent(bossPointParent);

        transform.position = patrolPoints[patrolPointIndex].transform.position;
    }

    
    void Update()
    {
        Move();
    }

    private void Move()
    {

        if (patrolPointIndex <= patrolPoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolPointIndex].transform.position, moveSpeed * Time.deltaTime);
        }

        if (PatrolPointIndex != patrolPoints.Length && transform.position == patrolPoints[patrolPointIndex].transform.position)
        {
            patrolPointIndex += 1;
        }

    }

    private void chooseParent(Transform patrolPointParent)
    {
        patrolPoints = new Transform[patrolPointParent.childCount];
        for (int i = 0; i < patrolPointParent.childCount; i++)
        {
            patrolPoints[i] = patrolPointParent.GetChild(i).transform;
        }
    }
}
