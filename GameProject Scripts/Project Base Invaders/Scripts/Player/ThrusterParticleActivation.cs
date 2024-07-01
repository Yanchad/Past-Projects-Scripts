using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ThrusterParticleActivation : MonoBehaviour
{
    PlayerController playerController;
    OffscreenEngineToggle offScreenEngineToggle;
    Fuel fuel;
    Controls controls;
    Abilities abilities;

    [SerializeField] private GameObject mainThruster;
    [SerializeField] private GameObject rcs_Right;
    [SerializeField] private GameObject rcs_Left;
    [SerializeField] private GameObject emergencyThruster;
    ParticleSystem mainThrusterPS;
    ParticleSystem rcs_RightPS;
    ParticleSystem rcs_LeftPS;
    ParticleSystem emergencyThrusterPS;
    

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        offScreenEngineToggle = GetComponent<OffscreenEngineToggle>();
        fuel = GetComponent<Fuel>();
        controls = GetComponent<Controls>();
        abilities = GetComponent<Abilities>();
    }
    void Start()
    {
        mainThrusterPS = mainThruster.gameObject.GetComponent<ParticleSystem>();
        rcs_RightPS = rcs_Right.gameObject.GetComponent<ParticleSystem>();
        rcs_LeftPS = rcs_Left.gameObject.GetComponent<ParticleSystem>();
        emergencyThrusterPS = emergencyThruster.gameObject.GetComponent<ParticleSystem>();

        mainThrusterPS.Stop();
        rcs_RightPS.Stop();
        rcs_LeftPS.Stop();
        emergencyThrusterPS.Stop();
    }
    private void Update()
    {
        ActivateParticles();
    }


    private void ActivateParticles()
    {
        if(fuel.FuelEmpty == false && offScreenEngineToggle.IsOutOfBounds == false)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                rcs_RightPS.Play();
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                rcs_RightPS.Stop();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                rcs_LeftPS.Play();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                rcs_LeftPS.Stop();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                mainThrusterPS.Play();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                mainThrusterPS.Stop();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && abilities.HasEmergencyThrusters && abilities.CanUse == true)
            {
                emergencyThrusterPS.Play();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                emergencyThrusterPS.Stop();
            }
        }
        if(fuel.FuelEmpty == true)
        {
            mainThrusterPS.Stop();
            rcs_LeftPS.Stop();
            rcs_RightPS.Stop();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "OutOfBounds")
        {
            mainThrusterPS.Stop();
            rcs_LeftPS.Stop();
            rcs_RightPS.Stop();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OutOfBounds" && controls.IsThrusting)
        {
            mainThrusterPS.Play();
        }
        if (collision.gameObject.tag == "OutOfBounds" && controls.IsRotatingLeft)
        {
            rcs_RightPS.Play();
        }
        if (collision.gameObject.tag == "OutOfBounds" && controls.IsRotatingRight)
        {
            rcs_LeftPS.Play();
        }

    }
}
