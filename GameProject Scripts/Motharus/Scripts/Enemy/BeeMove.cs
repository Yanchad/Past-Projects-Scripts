using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BeeMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(transform.right.x * moveSpeed, rb.velocity.y);
    }
}
