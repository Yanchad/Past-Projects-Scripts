using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfWaypoint : MonoBehaviour
{

    FollowThePath followThePath;

    void Start()
    {
        followThePath = GetComponent<FollowThePath>();
    }

    void Update()
    {
        if (followThePath.WaypointIndex >= followThePath.Waypoints.Length)
        {
            Destroy(gameObject);
        }
    }
}
