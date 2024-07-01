
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    OffscreenEngineToggle offscreenEngineToggle;
    Controls controls;
    GroundCheck groundCheck;
    Fuel fuel;

    private Rigidbody2D rb;
    public Rigidbody2D RB { get { return rb; } set { rb = value; } }

    [Tooltip("Thruster force")]
    [SerializeField] private float thrustForce;
    [Tooltip("Maneuverability or the Rotation Speed of the ship")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float minRotationSpeed;
    [SerializeField] private float maxRotationSpeed;
    [Tooltip("The maximum speed the player can go")]
    [SerializeField] private float maxSpeed = 10;
    private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    public float ThrustForce {  get { return thrustForce; } set {  thrustForce = value; } }
    

    void Awake()
    {
        offscreenEngineToggle = GetComponent<OffscreenEngineToggle>();
        controls = GetComponent<Controls>();
        groundCheck = GetComponent<GroundCheck>();
        fuel = GetComponent<Fuel>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rotateSpeed = Mathf.Clamp(rotateSpeed, minRotationSpeed, maxRotationSpeed);
        Movement();
        Rotate();
        EdgeTeleport();

    }


    private void Movement()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        moveSpeed = rb.velocity.magnitude;

        if(controls.IsThrusting == true && offscreenEngineToggle.IsOutOfBounds == false && fuel.FuelEmpty == false)
        {
            rb.AddForce(transform.up * thrustForce * Time.deltaTime, ForceMode2D.Impulse);

        }
    }
    private void Rotate()
    {
        if (controls.IsRotatingLeft == true && groundCheck.IsGrounded == false && fuel.FuelEmpty == false)
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
        if (controls.IsRotatingRight == true && groundCheck.IsGrounded == false && fuel.FuelEmpty == false)
        {
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        }
    }

    private void EdgeTeleport()
    {
        Vector3 pos = transform.position;
        if(pos.x < -15.5f)
        {
            transform.position = new Vector3(15.5f, pos.y, pos.z);
        }

        if(pos.x > 15.5f)
        {
            transform.position = new Vector3(-15.5f, pos.y, pos.z);
        }
    }
}
