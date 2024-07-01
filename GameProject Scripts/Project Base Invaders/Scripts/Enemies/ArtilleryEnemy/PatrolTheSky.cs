using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTheSky : MonoBehaviour
{
    Alarm alarm;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private int patrolPointIndex = 0;
    
    private Transform patrolPointParent1;
    private bool reverseMovement;

    public Transform[] PatrolPoints { get { return patrolPoints; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public int PatrolPointIndex => patrolPointIndex;


    private void Awake()
    {
        alarm = FindObjectOfType<Alarm>();
    }
    void Start()
    {
        patrolPointParent1 = GameObject.Find("PatrolPointParent1").transform;

        if (alarm.greenhouseAlarm.activeInHierarchy || alarm.h3MineAlarm.activeInHierarchy || alarm.researchLabAlarm.activeInHierarchy || alarm.mechanicAlarm.activeInHierarchy) 
        {
            chooseParent(patrolPointParent1);
        } 

        transform.position = patrolPoints[patrolPointIndex].transform.position;
    }
    
    void Update()
    {
        Move();
    }


    private void Move()
    {
        // Check if patrolPointIndex is within a valid range
        if (patrolPointIndex >= 0 && patrolPointIndex < patrolPoints.Length)
        {
            // Move towards the current patrol point
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolPointIndex].transform.position, moveSpeed * Time.deltaTime);

            // Check if the object has reached the current patrol point
            if (transform.position == patrolPoints[patrolPointIndex].transform.position)
            {
                // If reverseMovement is true and the object is at the last patrol point, reverse the movement
                if (reverseMovement && patrolPointIndex == patrolPoints.Length - 1)
                {
                    patrolPointIndex -= patrolPoints.Length - 2 ;
                }
                else
                {
                    // Otherwise, move to the next patrol point
                    patrolPointIndex += 1;

                    // Handle reversing when reaching the first patrol point after reversing
                    if (reverseMovement && patrolPointIndex < 0)
                    {
                        patrolPointIndex = 1; // Set to 1 to avoid an out-of-bounds index
                    }
                }
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PatrolPoint")
        {
            Debug.Log("PatrolPoint reached");
            reverseMovement = true;
        }
        if (collision.gameObject.tag == "PatrolPointStart")
        {
            reverseMovement = false;
        }
    }
}
