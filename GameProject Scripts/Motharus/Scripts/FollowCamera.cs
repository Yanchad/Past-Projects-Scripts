using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTarget;
    private Vector3 currentVelocity;


    void Update()
    {
        Vector3 targetPos = cameraTarget.position;
        
        targetPos.z = -10;
        targetPos.x = 0;
        targetPos.y += 1;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, 0.5f);
    }
}
