using System;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PlayerKeyBinds))]

public class PlayerMove : MonoBehaviour
{
    #region Variables&others

    //Other
    private PlayerKeyBinds playerKeyBinds;
    private Rigidbody rb;

    
    [Header("Assignables")]
    //Assignables
    [SerializeField] private GrapplingGun grapplingGun;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform orientation;

    [Header("Adjustable Settings")]

    //[Header("Mouse")]
    //Rotation and look
    private SensitivityManager sensManager;


    [Header("Movement")]
    //Movement
    [SerializeField] private float originalMovementSpeed = 4500;
    [Tooltip("Works? maybe? Anyway lowering this, the player doesn't go as fast when bhopping and stuff")]
    [SerializeField] private float allMaxSpeed = 150;
    [Tooltip("Max speed when walking")]
    [SerializeField] private float maxSpeed = 20;
    [Tooltip("Force to make sure the player doesn't slide when not pressing anything")]
    [SerializeField] private float counterMovement = 0.175f;
    [Tooltip("Depending on the slope angle, the player can walk on different slopes without entering wallrun")]
    [SerializeField] private float maxSlopeAngle = 35f;
    [Tooltip("How fast the player decelerates after exiting high speeds")]
    [SerializeField] private float decelerationRate = 50f;
    [Tooltip("How fast the player falls while on air")]
    [SerializeField] private float extraAirGravity = 10f;
    [SerializeField] LayerMask groundLayerMask;

    [Header("Jumping")]
    //Jumping
    [SerializeField] private float jumpCD = 0.25f;
    [SerializeField] private float jumpForce = 550f;
    [SerializeField] private float doubleJumpForce = 20f;
    [Tooltip("Double Jump Forward Force")]
    [SerializeField] private float dblJumpFWforce = 5f;
    [SerializeField] private float doubleJumpMultiplier = 1f;

    [Header("Double Jump Detection")]
    // Raycast
    [SerializeField] private Vector3 rayOffset;
    [SerializeField] private float rayLength = 1f;

    [Header("Crouch & Slide")]
    //Crouch & Slide
    //[SerializeField] private float slideForce = 400f;
    [Tooltip("Forward Impulsive force at the start of slide")]
    [SerializeField] private float slideForceImpulse = 100f;
    [SerializeField] private float slideCounterMovement = 0.2f;


    [Header("Wall run")]
    //Wallrun
    [SerializeField] private float wallRunSpeedBoost = 5f;
    [Tooltip("How frequently you can jump when wallrunning")]
    [SerializeField] private float wallJumpCD = 1f;
    [SerializeField] private float wallJumpFwdForce = 50f;
    [SerializeField] private float wallJumpSideForce = 50f;
    [SerializeField] private float wallJumpUpForce = 10f;

    [Header("Glide")]
    //Gliding
    [SerializeField] private float onGlideMass;
    [SerializeField] private float onGroundMass;
    [SerializeField] private float glideSpeed;
    [Tooltip("An upward force to counter gravity")]
    [SerializeField] private float glideUpwardForce = 100f;
    [SerializeField] private float glideDuration = 2f;
    [SerializeField] private float glideCooldownDuration = 5f;



    [Header("Read Only")]
    // ReadOnly Movement
    [SerializeField] private float currentMovementSpeed;
    [SerializeField] private float currentMaxSpeed;
    [SerializeField] private bool grounded;
    private float threshold = 0.01f;
    private bool isSprinting = false;
    private float airGravityZero = 0f;
    private float airGravity;
    // ReadOnly Jumping
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private bool isCloseToGround;
    [SerializeField] private bool doubleJump = true;
    private bool readyToJump = true;
    // ReadOnly Rotation and look
    private float xRotation;
    private float sensMultiplier = 1f;
    // ReadOnly Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;
    // ReadOnly Sliding
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;
    private bool isCrouching;
    // ReadOnly Wallrunning
    [SerializeField] private bool wallRunning;
    private bool wallrunSoundPlayed = false;
    //private bool surfing;
    private Vector3 wallRunPos;
    private float actualWallRotation;
    private float wallRotationVel;
    private float desiredX;
    private float wallRunGravity = 1f;
    private float wallRunRotation;
    private bool cancelling;
    private bool readyToWallrun = true;
    private bool cancellingGrounded;
    private bool cancellingWall;
    private bool cancellingSurf;
    // ReadOnly Gliding
    [SerializeField] private bool isGliding;
    [SerializeField] private bool canGlide;
    [SerializeField] private float glideTimer;
    [SerializeField] private float glideCooldownTimer;
    [SerializeField] private bool isGlideCooldown = false;
    // Relative To Look Vel variables
    private float lookAngle;
    private float moveAngle;
    private float yMag;
    private float xMag;
    private float u;
    private float v;

    // Getters
    public bool Grounded => grounded;
    public bool Wallrunning => wallRunning;
    public bool ReadyToJump => readyToJump;
    public bool CanDoubleJump => canDoubleJump;
    public bool DoubleJump => doubleJump;
    public bool IsGliding => isGliding;
    public bool IsCrouching => isCrouching;

    #endregion Variables&others

    #region OnGlideTimeChanged

    private List<IOnGlideTimeChanged> OnGlideTimeChanged = new List<IOnGlideTimeChanged>();
    public void RegisterListener(IOnGlideTimeChanged listener)
    {
        OnGlideTimeChanged.Add(listener);
    }
    public void RemoveListener(IOnGlideTimeChanged listener)
    {
        OnGlideTimeChanged.Remove(listener);
    }
    public void Invoke(float time)
    {
        for (int i = 0; i < OnGlideTimeChanged.Count; i++)
        {
            OnGlideTimeChanged[i].OnGlideTimeChanged(time);
        }
    }

    #endregion OnGlideTimeChanged

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        playerKeyBinds = GetComponent<PlayerKeyBinds>();
        sensManager = FindObjectOfType<SensitivityManager>();

        currentMovementSpeed = originalMovementSpeed;
        currentMaxSpeed = maxSpeed;
        playerScale = transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        glideTimer = glideDuration;
    }
    private void LateUpdate()
    {
        WallRunning();
    }
    void FixedUpdate()
    {
        Movement();
        SmoothDeceleration();
        Sliding();
    }
    private void Update()
    {
        Glide();
        MyInput();
        Look();
        DoubleJumpRayCast();
    }


    #region Crouch&Slide

    private void MyInput()
    {
        //Crouching
        if (Input.GetKeyDown(playerKeyBinds.CrouchSlideBtn))
        {
            StartCrouch();
        }
        if (Input.GetKeyUp(playerKeyBinds.CrouchSlideBtn))
        {
            StopCrouching();
        }
    }

    private void StartCrouch()
    {
        isCrouching = true;
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        if (rb.velocity.magnitude > 0.5f)
        {
            if (grounded)
            {
                rb.AddForce(orientation.transform.forward * slideForceImpulse, ForceMode.Impulse);
            }
        }
    }
    private void StopCrouching()
    {
        isCrouching = false;
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Sliding()
    {
        if (isCrouching && grounded)
        {
            if (rb.velocity.magnitude > 0.5f)
            {
                Vector2 mag = FindVelRelativeToLook();
                float xMag = mag.x, yMag = mag.y;

                if (playerKeyBinds.Xmove > 0 && xMag > currentMaxSpeed) playerKeyBinds.Xmove = 0;
                if (playerKeyBinds.Xmove < 0 && xMag < -currentMaxSpeed) playerKeyBinds.Xmove = 0;
                if (playerKeyBinds.Ymove > 0 && yMag > currentMaxSpeed) playerKeyBinds.Ymove = 0;
                if (playerKeyBinds.Ymove < 0 && yMag < -currentMaxSpeed) playerKeyBinds.Ymove = 0;

                rb.AddForce(orientation.transform.forward * playerKeyBinds.Ymove * currentMovementSpeed * Time.deltaTime);
                rb.AddForce(orientation.transform.right * playerKeyBinds.Xmove * currentMovementSpeed * Time.deltaTime, ForceMode.Force);
            }
        }
    }

    #endregion Crouch&Slide

    #region Movement

    private void Movement()
    {
        
        //Extra Gravity
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;
        
        //Counteract sliding and sloppy movement
        CounterMovement(playerKeyBinds.Xmove, playerKeyBinds.Ymove, mag);

        //If holding jump && ready to jump, then jump
        if (readyToJump && playerKeyBinds.Jump) 
        {
            Jump();
        } 


        //if sliding down a ramp, add force down so player stays grounded and also builds speed
        if (playerKeyBinds.Crouch && grounded && readyToJump)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }

        //If speed is larger than maxSpeed, cancel out the input so you don't go over max speed
        if (playerKeyBinds.Xmove > 0 && xMag > currentMaxSpeed) playerKeyBinds.Xmove = 0;
        if (playerKeyBinds.Xmove < 0 && xMag < -currentMaxSpeed) playerKeyBinds.Xmove = 0;
        if (playerKeyBinds.Ymove > 0 && yMag > currentMaxSpeed) playerKeyBinds.Ymove = 0;
        if (playerKeyBinds.Ymove < 0 && yMag < -currentMaxSpeed) playerKeyBinds.Ymove = 0;


        //multipliers
        float multiplier = 1f, multiplierV = 1f;

        //Movement in air
        if (!grounded)
        {
            multiplier = 1f;
            multiplierV = 1f;
            rb.AddForce(Vector3.down * airGravity * Time.deltaTime);
        }

        //Movement while sliding
        if (grounded && playerKeyBinds.Crouch) multiplierV = 1f;

        // Gliding
        if (wallRunning || grounded) canGlide = false;


        //Apply forces to move player
        if (isGliding)
        {
            rb.AddForce(orientation.transform.forward * playerKeyBinds.Ymove * glideSpeed * Time.deltaTime);
            rb.AddForce(orientation.transform.right * playerKeyBinds.Xmove * glideSpeed * Time.deltaTime);
            rb.AddForce(Vector3.up * glideUpwardForce * Time.deltaTime);
        }
        else
        {
            rb.AddForce(orientation.transform.forward * playerKeyBinds.Ymove * currentMovementSpeed * Time.deltaTime * multiplier * multiplierV);
            rb.AddForce(orientation.transform.right * playerKeyBinds.Xmove * currentMovementSpeed * Time.deltaTime * multiplier);
        }

    }
    private void SmoothDeceleration()
    {
        u = Mathf.DeltaAngle(lookAngle, moveAngle);
        v = 90 - u;

        float magnitude = rb.velocity.magnitude;
        yMag = magnitude * Mathf.Cos(u * Mathf.Deg2Rad);
        xMag = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);
        if ((yMag > maxSpeed || xMag > maxSpeed) && !grounded)
        {
            // Set MaxSpeed (doesn't work but slows it down at least)
            if (magnitude >= allMaxSpeed)
            {
                currentMaxSpeed = allMaxSpeed;
            }
            else if (magnitude <= allMaxSpeed) currentMaxSpeed = magnitude;
        }
        else if ((grounded && currentMaxSpeed > maxSpeed) || (isSprinting && grounded && currentMaxSpeed > maxSpeed))
        {
            currentMaxSpeed -= decelerationRate * Time.deltaTime;
        }
    }
    #endregion Movement

    #region Jump

    private void Jump()
    {
        //Normal jump
        if (grounded && readyToJump && !Wallrunning)
        {
            readyToJump = false;

            //Add jump forces
            rb.AddForce(Vector2.up * jumpForce * rb.mass);
            rb.AddForce(normalVector * jumpForce * rb.mass * 0.5f);

            //If jumping while falling, reset y velocity.
            Vector3 vel = rb.velocity;

            if (rb.velocity.y < 0.5f) rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0) rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCD);
        }
        // WallJump
        if(wallRunning && readyToJump)
        {
            readyToJump = false;
            // Add jump forces when wall running
            rb.AddForce(playerCam.forward * wallJumpFwdForce * rb.mass);

            // Add force to either side to jump off from wall
            if(wallRunRotation < 0)
            {
                rb.AddForce(playerCam.right * wallJumpSideForce * rb.mass, ForceMode.Impulse);
                rb.AddForce(Vector3.up * wallJumpUpForce * rb.mass, ForceMode.Impulse);
            }
            else if(wallRunRotation > 0)
            {
                rb.AddForce(-playerCam.right * wallJumpSideForce * rb.mass, ForceMode.Impulse);
                rb.AddForce(Vector3.up * wallJumpUpForce * rb.mass, ForceMode.Impulse);
            }

            rb.AddForce(normalVector * jumpForce * 0.5f);

            //If jumping while falling, reset y velocity.
            Vector3 vel = rb.velocity;

            if (rb.velocity.y < 0.5f) rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0) rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), wallJumpCD);
        }

        // Double Jump
        // Set double jumping force based on speed
        float velMag = rb.velocity.magnitude;
        float force = doubleJumpForce + velMag * doubleJumpMultiplier;
        float fwdForce = dblJumpFWforce + velMag * doubleJumpMultiplier;

        if (!grounded && canDoubleJump && doubleJump && !wallRunning)
        {
            readyToJump = false;

            // Reset the velocity when double jumping. Allows changing direction with jumping
            rb.velocity = Vector3.zero;

            rb.AddForce(Vector3.up * force * rb.mass, ForceMode.Impulse);
            rb.AddForce(orientation.forward * fwdForce * rb.mass, ForceMode.Impulse);

            canDoubleJump = false;
            doubleJump = false;

            Invoke(nameof(ResetJump), jumpCD);
        }
        if (grounded || wallRunning) doubleJump = true;
    }
    private void DoubleJumpRayCast()
    {
        // Cast a ray to determine if you can double jump to prevent BHop failing as easily
        isCloseToGround = Physics.Raycast(transform.position + rayOffset, Vector3.down, rayLength, groundLayerMask);
        if (isCloseToGround) canDoubleJump = false;
        else if (!isCloseToGround) canDoubleJump = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + rayOffset, Vector3.down * rayLength);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    #endregion Jump

    #region Gliding

    private void Glide()
    {
        // CHECK IF THE PLAYER CAN GLIDE
        canGlide = !grounded && !wallRunning && !grapplingGun.IsGrappling && rb.velocity.y < 0;

        if (canGlide && rb.velocity.y < 0)
        {
            if (!isGlideCooldown && playerKeyBinds.Glide == true)
            {
                // Start Gliding

                airGravity = airGravityZero;
                rb.mass = onGlideMass;
                isGliding = true;
            }
        }

        // RESET GLIDE COOLDOWN
        if(grounded || wallRunning)
        {
            isGlideCooldown = false;
            glideCooldownTimer = 0f;
            glideTimer = glideDuration;
        }

        // UPDATE GLIDE TIMER
        if (isGlideCooldown)
        {
            glideCooldownTimer -= Time.deltaTime;

            if(glideCooldownTimer <= 0f)
            {
                isGlideCooldown = false; 

                if (canGlide)
                {
                    glideTimer = glideDuration;
                }
            }
        }
        else if (isGliding)
        {
            glideTimer -= Time.deltaTime;

            if (glideTimer <= 0f)
            {
                // Stop Gliding
                rb.mass = onGroundMass;
                airGravity = extraAirGravity;
                isGliding = false;
                isGlideCooldown = true; 
                glideCooldownTimer = glideCooldownDuration; 
            }
        }

        // RESET FLAGS IF CONDITIONS ARE NOT MET
        if (!canGlide || playerKeyBinds.Glide == false || grapplingGun.IsGrappling)
        {
            rb.mass = onGroundMass;
            airGravity = extraAirGravity;
            isGliding = false;
        }

        // INVOKE GLIDETIMER
        Invoke(glideTimer / glideDuration);
    }
    #endregion Gliding

    #region MouseLook
    private void Look()
    {
        if(!pauseMenu.activeInHierarchy)
        {
            // Get the sensitivity value from the slider
            float currentSensitivity = sensManager.Sensitivity;

            float mouseX = Input.GetAxis("Mouse X") * currentSensitivity * Time.fixedDeltaTime * sensMultiplier;
            float mouseY = Input.GetAxis("Mouse Y") * currentSensitivity * Time.fixedDeltaTime * sensMultiplier;

            //Find current look rotation
            Vector3 rot = playerCam.transform.localRotation.eulerAngles;
            desiredX = rot.y + mouseX;

            //Rotate, and also make sure we don't over- or under rotate.
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //Perform the rotations
            FindWallRunRotation();
            actualWallRotation = Mathf.SmoothDamp(actualWallRotation, wallRunRotation, ref wallRotationVel, 0.2f);
            playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, actualWallRotation);
            orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
        }
    }
    #endregion MouseLook

    #region CounterMovement

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || playerKeyBinds.Jump || grapplingGun.IsGrappling) return;

        //Slow down sliding
        if (playerKeyBinds.Crouch)
        {
            rb.AddForce(currentMovementSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(originalMovementSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(originalMovementSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        // Limit diagonal running without full stop
        float currentSpeed = Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2));

        if (currentSpeed > currentMaxSpeed)
        {
            float fallspeed = rb.velocity.y;
            float ratio = currentMaxSpeed / currentSpeed;
            Vector3 n = new Vector3(rb.velocity.x * ratio, fallspeed, rb.velocity.z * ratio);
            rb.velocity = n;
        }
    }
    #endregion CounterMovement

    #region VelocityRelativeToLook

    /// Find the velocity relative to where the player is looking
    /// Useful for vectors calculations regarding movement and limiting movement
    public Vector2 FindVelRelativeToLook()
    {
        lookAngle = orientation.transform.eulerAngles.y;
        moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        u = Mathf.DeltaAngle(lookAngle, moveAngle);
        v = 90 - u;

        float magnitude = rb.velocity.magnitude;
        yMag = magnitude * Mathf.Cos(u * Mathf.Deg2Rad);
        xMag = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }
    #endregion VelocityRelativeToLook

    #region Wallrunning

    private void FindWallRunRotation()
    {
        if (!wallRunning)
        {
            wallRunRotation = 0f;
            return;
        }
        _ = new Vector3(0f, playerCam.transform.rotation.y, 0f).normalized;
        new Vector3(0f, 0f, 1f);
        float num = 0f;
        float current = playerCam.transform.rotation.eulerAngles.y;

        if (Math.Abs(wallNormalVector.x - 1f) < 0.1f)
        {
            num = 90f;
        }
        else if(Math.Abs(wallNormalVector.z - 1f) < 0.1f)
        {
            num = 270f;
        }
        else if (Math.Abs(wallNormalVector.z - -1f) < 0.1f)
        {
            num = 180f;
        }
        num = Vector3.SignedAngle(new Vector3(0f, 0f, 1f), wallNormalVector, Vector3.up);
        float num2 = Mathf.DeltaAngle(current, num);
        wallRunRotation = (0f - num2 / 90f) * 15f;
        if(!readyToWallrun)
        {
            return;
        }

        if((Mathf.Abs(wallRunRotation) < 4f && playerKeyBinds.Ymove > 0f && Math.Abs(playerKeyBinds.Xmove) < 0.1f) || Mathf.Abs(wallRunRotation) > 22f && playerKeyBinds.Ymove < 0f && Math.Abs(playerKeyBinds.Xmove) < 0.1f)
        {
            if (!cancelling)
            {
                cancelling = true;
                CancelInvoke("CancelWallrun");
                Invoke("CancelWallrun", 0.2f);
            }
        }
        else
        {
            cancelling = false;
            CancelInvoke("CancelWallrun");
        }
    }
    private void CancelWallrun()
    {
        Invoke("GetReadyToWallrun", 0.1f);
        rb.AddForce(wallNormalVector * 600f);
        readyToWallrun = false;

    }
    private void GetReadyToWallrun()
    {
        readyToWallrun = true;
    }

    private void WallRunning()
    {
        if (wallRunning && !isGliding)
        {
            if (!wallrunSoundPlayed)
            {
                //audioManager.PlaySFX(audioManager.wallTouch);
                //audioManager.PlaySFX(audioManager.wallSlide);
                wallrunSoundPlayed = true;
            }
            
            rb.AddForce(-wallNormalVector * Time.deltaTime * currentMovementSpeed);
            rb.AddForce(Vector3.up * Time.deltaTime * rb.mass * 50f * wallRunGravity);
            Vector3 camForward = playerCam.forward;
            camForward.y = 0f;
            rb.AddForce(camForward * Time.deltaTime * currentMovementSpeed * wallRunSpeedBoost);
        }
        else
        {
            //audioManager.StopSFX(audioManager.wallTouch);
            wallrunSoundPlayed = false;
        }
    }
    private void StartWallRun(Vector3 normal)
    {
        // Check if the player is not grounded and is ready to initiate a wall run
        if (!grounded && readyToWallrun)
        {
            // set the wall normal vector to the provided normal vector
            wallNormalVector = normal;

            // Set the upward force to be applied during the wall run
            float upwardForce = 20f;

            if (!wallRunning && !isGliding)
            {
                // Zero out the vertical component of the player's velocity and apply upward force
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
            }
            if(!wallRunning && isGliding)
            {
                upwardForce = 2f;
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
            }
            wallRunning = true;

        }
    }
    #endregion Wallrunning

    #region SurfaceChecks

    private bool IsFloor(Vector3 v)
    {
        return Vector3.Angle(Vector3.up, v) < maxSlopeAngle;
    }
    private bool IsWall(Vector3 v)
    {
        // Calculate the angle between the up vector (Vector3.up) and the input vector 'v'
        // Check if the absolute difference between the calculated angle and 81 degrees is greater than 0.1 degrees
        return Math.Abs(81f - Vector3.Angle(Vector3.up, v)) > .1f;
    }
    private bool isSurf(Vector3 v)
    {
        // Calculate the angle between the up vector (Vector3.up) and the input vector 'v'
        float num = Vector3.Angle(Vector3.up, v);

        // Check if the angle is less than 80 degrees
        if (num < 80f)
        {
            // If the angle is less than the maximum slope angle, return true (surface is surfable)
            return num > maxSlopeAngle;
        }
        return false;
    }

    private void OnCollisionStay(Collision collision)
    {
        // Get the layer of the collided GameObject
        int layer = collision.gameObject.layer;

        // Check if the collided GameObject's layer is not included in the 'groundLayerMask'
        if ((int)groundLayerMask != ((int)groundLayerMask | (1 << layer)))
        {
            // If not, return without doing anything
            return;
        }

        // Iterate through all contact points of the collision
        for (int i = 0; i < collision.contactCount; i++)
        {
            // Get the normal vector of the contact point
            Vector3 normal = collision.contacts[i].normal;

            // Check if the surface is considered a floor
            if (IsFloor(normal))
            {
                // Reset wallRunning flag if previously wall running
                if (wallRunning)
                {
                    wallRunning = false;
                }
                grounded = true;
                canGlide = false;

                // Store the normal vector of the floor
                normalVector = normal;

                cancellingGrounded = false;
                CancelInvoke("StopGrounded");
            }
            
            // Check if the surface is considered a wall (and on the "Ground" layer)
            if(IsWall(normal) && layer == LayerMask.NameToLayer("Ground"))
            {
                // Start wall run and reset some flags

                StartWallRun(normal);
                cancellingWall = false;
                canGlide = false;
                doubleJump = true;
                CancelInvoke("StopWall");
            }

            // Check if the surface is considered a surfable surface
            if (isSurf(normal))
            {
                // Set surfing flag to true and reset some flags
                //surfing = true;
                cancellingSurf = false;
                CancelInvoke("StopSurf");
            }
        }
        float num = 3f;

        // Set up delayed calls to stop grounded, wall, and surf states
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke("StopGrounded", Time.deltaTime * num);
        }
        if (!cancellingWall)
        {
            cancellingWall = true;
            Invoke("StopWall", Time.deltaTime * num);
        }
        if (!cancellingSurf)
        {
            cancellingSurf = true;
            Invoke("StopSurf", Time.deltaTime * num);
        }
    }
    private void StopGrounded()
    {
        grounded = false;
    }
    private void StopWall()
    {
        wallRunning = false;
        wallrunSoundPlayed = false;

    }
    private void StopSurf()
    {
        //surfing = false;
    }
    #endregion SurfaceChecks
}
