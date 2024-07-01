using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    [SerializeField] private float knockBackForce = 100f;



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if(playerRigidbody != null)
            {
                ContactPoint contact = collision.GetContact(0);

                Vector3 knockBackDirection = -contact.normal;
                playerRigidbody.AddForce(knockBackDirection * knockBackForce, ForceMode.Impulse);
            }
        }
    }
}
