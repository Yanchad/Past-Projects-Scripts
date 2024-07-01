using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Health hp;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector3 direction;
    private float totalSpeed;
    private float chargedPower;

    [SerializeField] private bool jumpNow = false;
    [SerializeField] public bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isGrabbing;

    [SerializeField] float movingForce = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float airSpeedMultiplier = 0.5f;

    [SerializeField] private float groundCheckRadius = 0.5f;
    [SerializeField] private Transform groundCheckOrigin;
    [SerializeField] private float grabCheckRadius = 0.5f;
    [SerializeField] private Transform grabCheckOrigin;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        totalSpeed = movingForce;
        animator = GetComponentInChildren<Animator>();

    }
    void Update()
    {
        Controls();
        GroundCheck();
        GrabCheck();
        FlipPlayer();
        SetAnimator();
    }
    private void FixedUpdate()
    {
        Movement();
        if (jumpNow)
        {
            rb.AddForce(Vector2.up * jumpForce * chargedPower, ForceMode2D.Impulse);
            jumpNow = false;
            chargedPower = 0;
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckOrigin.position, groundCheckRadius);
        Gizmos.DrawWireSphere(grabCheckOrigin.position, grabCheckRadius);
    }


    private void Controls()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            chargedPower += Time.deltaTime;
            isJumping = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpNow = true;
        }
        else isJumping = false;
    }


    private void Movement()
    {
        if (isGrounded == false) totalSpeed = movingForce * airSpeedMultiplier;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        direction = new Vector3(x, y, 0);
        rb.AddForce(x * Vector3.right * movingForce, ForceMode2D.Force);
        float maxSpeed = 7; //maxSpeed set to custom value so it doesn't accelerate to infinity
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        if (direction.x != 0) isRunning = true;
        else if (direction.x == 0) isRunning = false;
    }


    private void GroundCheck() //Checking if you are on the ground
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckOrigin.position, groundCheckRadius);
        bool wasGrounded = isGrounded;
        isGrounded = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                isGrounded = true;
                isFalling = false;
                if (wasGrounded == false) isJumping = false;
            }
            else
            {
                isFalling = true;
            }
        }
    }


    private void GrabCheck() //Checking if a wall is grabbed
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(grabCheckOrigin.position, grabCheckRadius);
        
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.gameObject.tag != "NoAnimTrigger")
            {
                isGrabbing = true;
            }
            else
            {
                isGrabbing = false;
            }
        }
    }


    private void FlipPlayer()
    {
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }


    private void SetAnimator()
    {
        if (isGrabbing == true && isGrounded == true && isRunning == true)
        {
            
            //isGrabbing
            animator.SetBool("isIdling", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isGrabbing", true);
        }
        else if (isFalling == true)
        {
            //isFalling
            animator.SetBool("isIdling", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
            animator.SetBool("isGrabbing", false);
        }

        else if (direction.x == 0 && isJumping == false && isGrabbing == false)
        {
            //isIdling
            animator.SetBool("isIdling", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isGrabbing", false);
        }
        else if (isRunning == true && isJumping == false && isGrabbing == false)
        {
            //isRunning
            animator.SetBool("isIdling", false);
            animator.SetBool("isRunning", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isGrabbing", false);
        }
        else if (isGrounded == false && isJumping == true)
        {
            //isJumping
            animator.SetBool("isIdling", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
            animator.SetBool("isGrabbing", false);
        }
    }
}