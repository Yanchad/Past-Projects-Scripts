using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfWayPointCloak : MonoBehaviour
{
    FollowThePathCloak followThePathCloak;



    void Start()
    {
        followThePathCloak = GetComponent<FollowThePathCloak>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followThePathCloak.WayPointIndex >= followThePathCloak.WayPoints.Length)
        {
            Destroy(gameObject);
        }
    }
}
