using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateClock : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float yVel;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        rb.angularVelocity = new Vector3(0, yVel * Time.deltaTime, 0);
    }
}
