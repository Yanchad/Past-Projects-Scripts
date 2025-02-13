using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class PlayerShoot : MonoBehaviour
{
    PlayerHealth playerHealth;

    [Header("Inputs")]
    [SerializeField] private InputActionReference pointerPosition;
    [SerializeField] private InputActionReference shoot;

    [Header("Assignables")]
    [SerializeField] private Transform gunParent;
    [SerializeField] private Transform betterGunParent;
    [SerializeField] private GameObject normalGun;
    [SerializeField] private GameObject betterGun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject betterBullet;
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform betterBarrel1;
    [SerializeField] private Transform betterBarrel2;
    [SerializeField] private GameObject shield;
    [SerializeField] private LayerMask obstacleDetectionLayerMask;
    [SerializeField] private Transform betterGunDetectionPoint;

    [Header("Shooting Settings")]
    [SerializeField] private float fireRate = 0.2f; // Time between shots

    [Header("Audio")]
    [SerializeField] private GameObject shootSound;

    private Vector2 pointerDirection;
    private Vector2 barrelPosition;
    private float nextFireTime = 0f; // Timer to manage fire rate

    private bool hasBetterBullets = false;
    private bool hasBetterGun = false;
    private bool hasShieldUpgrade = false;
    [SerializeField] private bool canShoot = false;
    public bool HasBetterBullets { get { return hasBetterBullets; } set { hasBetterBullets = value; } }
    public bool HasBetterGun { get { return hasBetterGun; } set { hasBetterGun = value; } }
    public bool HasShieldUpgrade {  get { return hasShieldUpgrade; } set { hasShieldUpgrade = value; } }
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        hasBetterBullets = false;
        hasBetterGun = false;
        hasShieldUpgrade = false;
    }
    private void OnEnable()
    {
        shoot.action.Enable();
        pointerPosition.action.Enable();
    }
    private void OnDisable()
    {
        shoot.action.Disable();
        pointerPosition.action.Disable();
    }

    private void Update()
    {
        Inputs();
        RotateGun();
        ObstacleDetection();
        if (!playerHealth.IsDead)
        {
            if (hasBetterGun)
            {
                betterGun.SetActive(true);
                normalGun.SetActive(false);
            }
            else if (!hasBetterGun)
            {
                betterGun.SetActive(false);
                normalGun.SetActive(true);
            }
        }
        // Check if the shoot action is held down and fire rate allows shooting
        if(shoot.action.ReadValue<float>() > 0 && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
        if (hasShieldUpgrade && !shield.activeInHierarchy)
        {
            shield.SetActive(true);
        }
    }


    private void Inputs()
    {
        pointerDirection = GetPointerInput();
    }
    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return worldMousePos - (Vector2)transform.position;
    }
    private void RotateGun()
    {
        // Rotate Gun around the player
        Vector3 pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction2 = (pointerPos - transform.position).normalized;

        if (hasBetterGun)
        {
            betterGunParent.transform.right = direction2;
        }
        else
        {
            gunParent.transform.right = direction2;
        }

    }
    private void Shoot()
    {
        if (canShoot)
        {
            if (hasBetterGun)
            {
                if (hasBetterBullets)
                {
                    Instantiate(betterBullet, betterBarrel1.transform.position, Quaternion.identity);
                    Instantiate(betterBullet, betterBarrel2.transform.position, Quaternion.identity);
                    Instantiate(shootSound, barrel.transform.position, Quaternion.identity); // Sound
                    Instantiate(shootSound, barrel.transform.position, Quaternion.identity); // Sound
                }
                else
                {
                    Instantiate(bullet, betterBarrel1.transform.position, Quaternion.identity);
                    Instantiate(bullet, betterBarrel2.transform.position, Quaternion.identity);
                    Instantiate(shootSound, barrel.transform.position, Quaternion.identity); // Sound
                    Instantiate(shootSound, barrel.transform.position, Quaternion.identity); // Sound
                }
            }
            else
            {
                if (hasBetterBullets)
                {
                    Instantiate(betterBullet, barrel.transform.position, Quaternion.identity);
                    Instantiate(shootSound, barrel.transform.position, Quaternion.identity); // Sound
                }
                else
                {
                    Instantiate(bullet, barrel.transform.position, Quaternion.identity);
                    Instantiate(shootSound, barrel.transform.position, Quaternion.identity); // Sound
                }
            }
        }
    }

    private void ObstacleDetection()
    {
        // Define the start (barrel) and end (player) points of the ray
        if (hasBetterGun)
        {
            barrelPosition = betterGunDetectionPoint.position;
        }
        else
        {
            barrelPosition = barrel.position;
        }
        Vector2 playerPosition = transform.position;

        // Calculate the direction from the barrel towards the player
        Vector2 direction = (playerPosition - barrelPosition).normalized;

        // Calculate the distance from the barrel to the player
        float distance = Vector2.Distance(barrelPosition, playerPosition);

        // Raycast
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(obstacleDetectionLayerMask);
        contactFilter.useTriggers = false;  // Ignore trigger colliders
        RaycastHit2D[] hitResults = new RaycastHit2D[1];  // Array to store raycast results (only one needed)
        int hitCount = Physics2D.Raycast(barrelPosition, direction, contactFilter, hitResults, distance);
        RaycastHit2D hitInfo = hitCount > 0 ? hitResults[0] : default;

        // Check if the ray hit something
        if (hitCount > 0 && hitInfo.collider != null)
        {
            // If the hit object is not the player
            if (hitInfo.collider.gameObject != this.gameObject)
            {
                canShoot = false;
                Debug.DrawLine(barrelPosition, hitInfo.point, Color.red);  // Red line if an obstacle is detected
            }
            else
            {
                canShoot = true;
                Debug.DrawLine(barrelPosition, playerPosition, Color.green);  // Green line if no obstacle is detected
            }
        }
    }

}
