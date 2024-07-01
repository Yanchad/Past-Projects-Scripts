using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class GrapplingGun : MonoBehaviour
{
    PlayerKeyBinds playerKeyBinds;
    PlayerDeath playerDeath;
    PlayerAudio playerAudio;

    [Header("Assignables")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private LayerMask whatIsGrappleable;
    [SerializeField] private Transform gunTip;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private Image playerCrosshair;


    public bool IsGrappling => isGrappling;
    [Header("Grappling Settings")]
    [SerializeField] private float upForce = 10f;
    [SerializeField] private float FWD_Jump_Force = 100f;
    [SerializeField] private float FWD_Continous_Force = 1000f;
    [SerializeField] private float maxDistance = 100;
    [SerializeField] private float sphereMaxRadius = 100f;
    [SerializeField] private float sphereMinRadius = 1f;
    [SerializeField] private float grappleCooldown;

    [Header("ReadOnly")]
    [SerializeField] private bool isGrappling;
    private LineRenderer lineRenderer;
    private Vector3 grapplePoint;
    private SpringJoint joint;
    [SerializeField] private float currentHitDistance;
    [SerializeField] private float adjustedRadius;
    [SerializeField] private float stretchTimer;
    [SerializeField] private float stretchCooldownTimer;
    [SerializeField] private bool stretchCooldown = false;
    [SerializeField] private float grappleTimer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerKeyBinds = FindObjectOfType<PlayerKeyBinds>();
        playerDeath = FindObjectOfType<PlayerDeath>();
        playerAudio = FindObjectOfType<PlayerAudio>();
    }

    private void Start()
    {
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        CrosshairColorChange();
        StartStretchSound();
        if (Input.GetKeyDown(playerKeyBinds.GrappleBtn) && pauseMenu.activeInHierarchy == false && !playerDeath.OOB && !playerKeyBinds.Glide)
        {
            StartGrapple();
        }
        else if (Input.GetKeyUp(playerKeyBinds.GrappleBtn) && pauseMenu.activeInHierarchy == false || playerDeath.OOB && playerKeyBinds.Glide)
        {
            StopGrapple();
        }

        if(pauseMenu.activeInHierarchy) StopGrapple();
    }

    private void LateUpdate()
    {
        DrawRope();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(playerCam.position, playerCam.position * currentHitDistance);
        RaycastHit hit;
        float radius = GetAdjustedRadius(out hit);
        Gizmos.DrawWireSphere(playerCam.position + playerCam.forward * hit.distance, radius);
    }

    // Call whenever we want to start a grapple
    private void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.SphereCast(playerCam.position, GetAdjustedRadius(out hit), playerCam.forward, out hit, maxDistance, whatIsGrappleable))
        {
            //Audio
            //audioManager.PlaySFX(audioManager.grapple);
            playerAudio.Grapple.Play();


            lineRenderer.enabled = true;
            if (hit.transform.root == this.transform.root) { isGrappling = false; return; };


            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            // The distance grapple will try to keep from grapple point.
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // Change these values as seen fit
            joint.spring = 15f;
            joint.damper = 0f;
            joint.massScale = 1f;

            lineRenderer.positionCount = 2;
            
            Vector3 grappleDir = grapplePoint - transform.position;
            playerRB.velocity = Vector3.zero;
            playerRB.AddForce(Vector3.up * upForce * playerRB.mass, ForceMode.Impulse);
            playerRB.AddForce(playerCam.forward * FWD_Jump_Force * playerRB.mass, ForceMode.Impulse);
            playerRB.AddForce(playerCam.forward * FWD_Continous_Force * playerRB.mass);
            isGrappling = true;

        }
    }

    private void StartStretchSound()
    {
        if (isGrappling && !stretchCooldown)
        {
            stretchTimer += Time.deltaTime;
        }
        
        if (stretchTimer >= 2f)
        {
            //audioManager.PlaySFX(audioManager.grappleRopeStretch);
            playerAudio.GrappleRopeStretch.enabled = true;
            stretchTimer = 0;
            stretchCooldown = true;
        }

        //Set A cooldown for the audio
        if (stretchCooldown == true)
        {
            stretchCooldownTimer += Time.deltaTime;
        }
         if(stretchCooldownTimer >= 10f)
        {
            stretchCooldownTimer = 0f;
            stretchCooldown = false;
        }

    }

    // Calculate the adjusted radius based on distance
    private float GetAdjustedRadius(out RaycastHit hit)
    {
        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, maxDistance, whatIsGrappleable))
        {
            float distance = Vector3.Distance(playerCam.position, hit.point);
            adjustedRadius = Mathf.Lerp(sphereMinRadius, sphereMaxRadius, distance / maxDistance);
            return adjustedRadius;
        }
        else return sphereMinRadius;

    }

    // Call whenever we want to stop a grapple
    private void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;
        isGrappling = false;
        Destroy(joint);
        //playerAudio.Grapple.enabled = false;
        stretchTimer = 0f;
        playerAudio.GrappleRopeStretch.enabled = false;
    }

    void DrawRope()
    {
        if (!joint) return;
        if (!isGrappling) return;
        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    private void CrosshairColorChange()
    {
        RaycastHit hit;
        if (Physics.SphereCast(playerCam.position, GetAdjustedRadius(out hit), playerCam.forward, out hit, maxDistance, whatIsGrappleable))
        {
            currentHitDistance = hit.distance;
            playerCrosshair.color = Color.green;
        }
        else 
        {
            currentHitDistance = maxDistance;
            playerCrosshair.color = Color.red;
        }
    }
}
