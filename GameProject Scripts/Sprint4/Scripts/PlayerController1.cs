using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController1 : MonoBehaviour
{

    [Header("Left and Right Borders")]
    [SerializeField] private float mapBorderL = -2.66f;

    [SerializeField] private float mapBorderR = 2.64f;

    private Vector3 moveDirectionZ;
    private Vector3 moveDirectionX;

    private bool moveOnXLeft = false;
    private bool moveOnXRight = false;

    private Rigidbody rb;
    [SerializeField] private Transform foodRobotTF;
    [SerializeField] private Image buttonLeftIMG;
    [SerializeField] private Image buttonRightIMG;
    [SerializeField] private float acceleration = 4;
    public float Acceleration { get { return acceleration; } set { acceleration = value; } }
    [Header("Forward moving force")]
    [SerializeField] private float moveSpeedZ = 8f;
    public float MoveSpeedZ { get { return moveSpeedZ; } set {  moveSpeedZ = value; } }
    [Header("Horizontal moving force")]
    [SerializeField] private float moveSpeedX = 10;
    private float maxSpeed = 8;
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }

    [SerializeField] TextMeshProUGUI resetText;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        MyInput();
        GreyButton();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MyInput()
    {
        moveSpeedZ += acceleration * Time.deltaTime;
        moveSpeedZ = Mathf.Clamp(moveSpeedZ, 0, maxSpeed);
        moveDirectionZ = transform.forward * moveSpeedZ;
        moveDirectionX = transform.right * moveSpeedX;

        if (Input.GetKeyUp(KeyCode.Q))
        {
            foodRobotTF.localRotation = Quaternion.Euler(0, 0, 0f);
            moveOnXLeft = false;
        }
            if (Input.GetKeyUp(KeyCode.E))
        {
            foodRobotTF.localRotation = Quaternion.Euler(0, 0, 0f);
            moveOnXRight = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void MovePlayer()
    {
        if (rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * moveSpeedZ;
        }
        rb.AddForce(moveDirectionZ * moveSpeedZ, ForceMode.Force);
        

        if (Input.GetKey(KeyCode.Q))
        {
            moveOnXLeft = true;
        }
        else rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);

        if (Input.GetKey(KeyCode.E))
        {
            moveOnXRight = true;
        }
        else rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);



        if (moveOnXLeft == true && transform.position.x >= mapBorderL)
        {
            rb.AddForce(-moveDirectionX * moveSpeedX, ForceMode.Force);
            foodRobotTF.localRotation = Quaternion.Euler(0, -10, 0f);
        }
        if (moveOnXRight == true && transform.position.x <= mapBorderR)
        {
            rb.AddForce(moveDirectionX * moveSpeedX , ForceMode.Force);
            foodRobotTF.localRotation = Quaternion.Euler(0, 10, 0f);
        }
        if (moveOnXRight == false && moveOnXLeft == false)
        {
            foodRobotTF.localRotation = Quaternion.Euler(0, 0, 0f);
        }
    }
    private void GreyButton()
    {
        if(transform.position.x <= mapBorderL)
        {
            buttonLeftIMG.color = Color.grey;
        }
        else buttonLeftIMG.color = Color.white;
        if (transform.position.x >= mapBorderR)
        {
            buttonRightIMG.color = Color.grey;
        }
        else buttonRightIMG.color = Color.white;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Solid")
        {
            resetText.enabled = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Solid")
        {
            resetText.enabled = false;
        }
    }


    public void Left(bool _move)
    {
        moveOnXLeft = _move;
    }

    public void Right(bool _move)
    {
        moveOnXRight = _move;
    }
}
