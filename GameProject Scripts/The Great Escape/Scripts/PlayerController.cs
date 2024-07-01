using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    
    private GroundCheck groundCheck;

    public GroundCheck GroundCheck { get { return groundCheck; } }


    private float speed;
    private float gravity = -9.83f;

    [SerializeField] private float walkingSpeed = 5;
    [SerializeField] private float sprintSpeed = 10;
     
    [SerializeField] Vector3 velocity;
    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        groundCheck = GetComponent<GroundCheck>();
    }

    
    void Update()
    {
        if(groundCheck.IsGrounded && velocity.y < -2)
        {
            velocity.y = -2;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkingSpeed;
        }

        Vector2 controls = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 movement = transform.forward * controls.y + transform.right * controls.x;
        controller.Move(movement.normalized * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space) && groundCheck.IsGrounded)
        {
            velocity.y = 3;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
