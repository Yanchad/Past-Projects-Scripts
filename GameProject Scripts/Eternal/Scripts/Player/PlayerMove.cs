using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private float moveSpeed;
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;


    [Header("Inputs")]
    [SerializeField] private InputActionReference moveInput;

    public float MoveSpeed {  get { return moveSpeed; } set { moveSpeed = value; } }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        playerInput();
    }
    private void FixedUpdate()
    {
        movePlayer();
    }


    private void playerInput()
    {
        moveDirection = moveInput.action.ReadValue<Vector2>();
    }
    private void movePlayer()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
