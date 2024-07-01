using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    Controls controls;
    Fuel fuel;

    private Rigidbody2D rb;

    [Header("Emergency Thruster Settings")]
    [SerializeField] private bool hasEmergencyThrusters;
    [SerializeField] private float emergencyThrusterForce;
    [SerializeField] private float timer;
    [SerializeField] private float cooldown;
    private bool canUse;
    [Header("Audio")]
    [SerializeField] private AudioSource ETaudioSource;

    public float Timer { get { return timer; }set { timer = value; } }
    public bool CanUse { get { return canUse; } }
    public bool HasEmergencyThrusters { get { return hasEmergencyThrusters; } set { hasEmergencyThrusters = value; } }


    void Start()
    {
        controls = GetComponent<Controls>();
        fuel = GetComponent<Fuel>();    
        rb = GetComponent<Rigidbody2D>();
        hasEmergencyThrusters = false;
        timer = cooldown;
        canUse = false;
    }

    
    void Update()
    {
        EmergencyThruster();
    }

    private void EmergencyThruster()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            canUse = true;
        }else canUse = false;
        if (controls.UseAbility1 == true && timer <= 0 && hasEmergencyThrusters == true && fuel.FuelEmpty == false)
        {
            rb.velocity = new Vector3(0, 0);
            rb.AddForce(gameObject.transform.up * emergencyThrusterForce * Time.deltaTime, ForceMode2D.Impulse);
            ETaudioSource.Play();
            timer = cooldown;
        }
    }
}
