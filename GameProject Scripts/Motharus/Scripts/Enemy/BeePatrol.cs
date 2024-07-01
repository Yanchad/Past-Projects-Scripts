using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrol : MonoBehaviour
{

    [SerializeField] Transform wallCheck;
    [SerializeField] private float rayDistance = 0.3f;
   


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(wallCheck.position, transform.right * rayDistance);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, transform.right, rayDistance);
        if (hit)
        {
            transform.rotation *= Quaternion.Euler(0, 180, 0);
        }
    }
}
