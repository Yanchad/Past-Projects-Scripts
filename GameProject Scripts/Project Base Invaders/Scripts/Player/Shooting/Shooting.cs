using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    GroundCheck groundCheck;
    Controls controls;
    ActivateShield activateShield;
    [SerializeField] AudioSource audioSource;

    private Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] private GameObject ui_Mechanic;
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletStartPoint;
    [SerializeField] private bool canFire;
    private float timer;

    [SerializeField] private float fireRate;
    [SerializeField] float rotateSpeed;



    private void Awake()
    {
        groundCheck = FindObjectOfType<GroundCheck>();
        controls = GetComponentInParent<Controls>();
        activateShield = GetComponentInParent<ActivateShield>();
    }
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        ActivateBarrel();
    }


    private void ActivateBarrel()
    {
        if (groundCheck.IsGrounded && activateShield.ShieldIsActive == false && ui_Mechanic.activeInHierarchy == false)
        {
            barrel.SetActive(true);
            Shoot();
        }
        else barrel.SetActive(false);
    }
    private void Shoot()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -90, 90);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                canFire = true;
                timer = 0;
            }
        }
        if (controls.IsShooting && canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletStartPoint.position, Quaternion.identity);
            audioSource.Play();
        }
    }
}


