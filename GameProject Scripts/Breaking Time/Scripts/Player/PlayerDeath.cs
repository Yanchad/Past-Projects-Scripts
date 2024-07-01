using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Quaternion teleportPositionRot;

    [Header("Assignable")]
    [SerializeField] private Transform playerCam;

    [Header("Adjustable")]
    [Tooltip("How much you knock back from hitting a trap")]
    [SerializeField] private float knockBackForce = 100f;
    [Tooltip("How much you knock upwards after hitting a trap")]
    [SerializeField] private float knockBackUpForce = 50f;
    [SerializeField] private float outOfBoundsLimit;
    [SerializeField] private Vector3 startPosition;

    [Header("Read Only")]
    [SerializeField] private bool oob;
    [SerializeField] private bool hasVisitedCheckpoint;
    [SerializeField] private bool hasHitLeftSqueeze;
    [SerializeField] private bool hasHitRightSqueeze;
    [SerializeField] private Vector3 teleportPosition;

    public bool HasVisitedCheckpoint => hasVisitedCheckpoint;
    public bool OOB => oob;
    
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hasVisitedCheckpoint = false;
    }

    private void Start()
    {
        rb.transform.position = startPosition;    
    }

    private void Update()
    {
        // Set Out Of Bounds Limit
        if (rb.transform.position.y <= outOfBoundsLimit)
        {
            oob = true;
        }
        else oob = false;

        // Check if player Out Of Bounds and teleport
        if (oob && !hasVisitedCheckpoint)
        {
            rb.velocity = Vector3.zero;
            rb.transform.position = startPosition;
            playerCam.localRotation = Quaternion.Euler(0, 180, 0);

        }
        // Check if player has been to a Checkpoint and teleport there instead
        else if (oob && hasVisitedCheckpoint)
        {
            rb.velocity = Vector3.zero;
            rb.transform.position = teleportPosition;
            playerCam.localRotation = teleportPositionRot;
        }
        DeathBySqueezeTrap();
    }

    private void DeathBySqueezeTrap()
    {
        if(hasHitLeftSqueeze && hasHitRightSqueeze)
        {
            rb.velocity = Vector3.zero;
            rb.transform.position = teleportPosition;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        // If player has been to a Checkpoint set teleportPosition to checkpoint position
        if (collision.gameObject.tag == "Checkpoint")
        {
            teleportPosition = collision.GetComponentInChildren<Transform>().transform.position;
            teleportPositionRot = collision.GetComponent<Transform>().transform.rotation;
            hasVisitedCheckpoint = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DeathTrap")
        {
            rb.velocity = Vector3.zero;
            rb.transform.position = teleportPosition;
        }
        if(collision.gameObject.tag == "SqueezeTrapL")
        {
            hasHitLeftSqueeze = true;
        }
        if(collision.gameObject.tag == "SqueezeTrapR")
        {
            hasHitRightSqueeze = true;
        }
        if (collision.gameObject.tag == "Trap")
        {
            ContactPoint contact = collision.GetContact(0);
            Vector3 knockBackDirection = contact.normal;
            rb.AddForce(knockBackDirection * knockBackForce, ForceMode.Impulse);
        }
        if(collision.gameObject.tag == "ClockTrap")
        {
            ContactPoint contact = collision.GetContact(0);
            Vector3 knockBackDirection = contact.normal;
            rb.AddForce(knockBackDirection * knockBackForce, ForceMode.Impulse);
            rb.AddForce(Vector3.up * knockBackUpForce, ForceMode.Impulse);
        }
    }



    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "SqueezeTrapL")
        {
            hasHitLeftSqueeze = false;
        }
        if(collision.gameObject.tag == "SqueezeTrapR")
        {
            hasHitRightSqueeze = false;
        }

    }

}
