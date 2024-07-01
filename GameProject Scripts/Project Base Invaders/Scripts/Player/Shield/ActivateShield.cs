using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShield : MonoBehaviour
{
    Controls controls;
    GroundCheck groundCheck;
    PlayerHealth playerHealth;

    [SerializeField] private GameObject shieldGO;
    private CircleCollider2D circleCollider;

    private bool shieldIsActive;
    public bool ShieldIsActive { get { return shieldIsActive; } set { shieldIsActive = value; } }

    private void Awake()
    {
        controls = GetComponent<Controls>();
        groundCheck = GetComponent<GroundCheck>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        circleCollider = shieldGO.GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        shieldGO.SetActive(false);
        shieldIsActive = false;
    }
    private void Update()
    {
        ToggleShield();
    }

    private void ToggleShield()
    {
        if (controls.IsShielding == true && groundCheck.IsGrounded == true && playerHealth.ShieldIsBroken == false)
        {
            shieldGO.SetActive(true);
            circleCollider.enabled = true;
            shieldIsActive = true;
        } else
        {
            shieldGO.SetActive(false);
            circleCollider.enabled = false;
            shieldIsActive = false;
        }
        if (controls.IsThrusting)
        {
            shieldGO.SetActive(false);
            circleCollider.enabled = false;
            shieldIsActive = false;
        }
    }
}
