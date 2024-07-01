using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfWaypointTank : MonoBehaviour
{
    FollowThePathTank followThePathTank;



    void Start()
    {
        followThePathTank = GetComponent<FollowThePathTank>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followThePathTank.WayPointIndex >= followThePathTank.WayPoints.Length)
        {
            Destroy(gameObject);
        }
    }
}
