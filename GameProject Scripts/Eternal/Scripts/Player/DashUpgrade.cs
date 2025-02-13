using UnityEngine;
using UnityEngine.InputSystem;

public class DashUpgrade : MonoBehaviour
{
    private PlayerMove playerMove;
    [SerializeField] private ParticleSystem dashTrailFX;

    [SerializeField] private float dashSpeedMultiplier = 2f;  // Speed multiplier for dash
    [SerializeField] private float dashTime = 0.1f;            // Duration of the dash
    [SerializeField] private float dashCooldown = 0.5f;        // Cooldown between dashes
    private float dashTimer = 0f;                              // Timer for dash duration
    private float cooldownTimer = 0f;                           // Timer for cooldown
    
    [SerializeField] private bool isDashing = false;                             // Is the player currently dashing?
    [SerializeField] private bool hasDash = false;

    [SerializeField] private InputActionReference dashInput;

    [Header("Audio")]
    [SerializeField] private GameObject dashSound;
    public bool HasDash { get { return hasDash; } set { hasDash = value; } }

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();

        if (dashTrailFX != null)
        {
            dashTrailFX.Stop();
        }
    }

    
    private void OnEnable()
    {
        if (dashInput != null)
        {
            dashInput.action.Enable();
            dashInput.action.performed += Dash;
        }
    }

    private void OnDisable()
    {
        if (dashInput != null)
        {
            dashInput.action.performed -= Dash;
            dashInput.action.Disable();
        }
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Trigger the dash if the cooldown is over and dash is not active
        if (cooldownTimer >= dashCooldown && !isDashing)
        {
            // Dash will be triggered when the input action is performed
            return; // Dash action is handled via the callback in Dash() method
        }

        // If dashing, reduce dash duration and reset player speed after dash ends
        if (isDashing)
        {
            dashTimer += Time.deltaTime;

            // End the dash after the dash time has passed
            if (dashTimer >= dashTime)
            {
                EndDash();
            }
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (hasDash)
        {
            // If cooldown has elapsed and dash is not already in progress, start dash
            if (cooldownTimer >= dashCooldown && !isDashing)
            {
                StartDash();
                if (dashSound != null) Instantiate(dashSound, transform.position, Quaternion.identity);
            }
        }
    }

    private void StartDash()
    {
        // Start Dash
        playerMove.MoveSpeed *= dashSpeedMultiplier;
        isDashing = true;
        dashTimer = 0f;
        cooldownTimer = 0f; // Reset cooldown after dash starts

        if(dashTrailFX != null)
        {
            dashTrailFX.Play();
        }
    }

    private void EndDash()
    {
        // Reset speed to normal and stop dashing
        playerMove.MoveSpeed /= dashSpeedMultiplier;
        isDashing = false;
        dashTimer = 0f;

        if (dashTrailFX != null)
        {
            dashTrailFX.Stop();
        }
    }
}
