using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Speedometer : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private TextMeshProUGUI speedTxt;
    [SerializeField] private Rigidbody playerRb;
    [Header("Read Only")]
    [SerializeField] private float speed;

    
    void FixedUpdate()
    {
        speed = playerRb.velocity.magnitude;

        speedTxt.text = speed.ToString("0.00");
    }
}
