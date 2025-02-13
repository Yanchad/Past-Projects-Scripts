using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [Header("Assignabled")]
    [SerializeField] private Transform shieldBase;
    [SerializeField] private Transform fakeShield;

    [SerializeField] private float rotationSpeed = 100f;


    private Transform player;
    PlayerHealth playerHealth;

    private Vector2 rotateDirection;

    [SerializeField] private Rigidbody2D invisShieldRb;

    private void Awake()
    {
        //invisShieldRb = fakeShield.GetComponentInChildren<Rigidbody2D>();
    }
    private void Start()
    {
        player = GameObject.Find("Player").gameObject.transform;
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        RotateShiel();
        RotateInvisShield();
    }

    private void RotateShiel()
    {
        if (player != null && gameObject.activeInHierarchy && !playerHealth.IsDead)
        {
            // Get the mouse position in world space
            Vector3 pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointerPos.z = 0f;  // Keep the shield rotation in 2D space (ignore Z-axis)

            // Calculate the direction from the player to the mouse position
            Vector2 direction2 = (pointerPos - player.position).normalized;

            // Calculate the opposite direction for the shield to face
            Vector2 oppositeDirection = -direction2;

            // Rotate the shield smoothly towards the opposite direction
            // This will make the shield face the opposite side of where the gun is aiming
            shieldBase.up = Vector2.Lerp(shieldBase.up, oppositeDirection, Time.fixedDeltaTime * rotationSpeed);
        }
    }
    //private void RotateInvisShield()
    //{
    //    if (player != null && gameObject.activeInHierarchy && !playerHealth.IsDead)
    //    {
    //        // Get the mouse position in world space
    //        Vector3 pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        pointerPos.z = 0f;  // Keep the shield rotation in 2D space (ignore Z-axis)

    //        // Calculate the direction from the player to the mouse position
    //        Vector2 direction2 = (pointerPos - player.position).normalized;

    //        // Calculate the opposite direction for the shield to face
    //        Vector2 oppositeDirection = -direction2;

    //        // Rotate the shield smoothly towards the opposite direction
    //        // This will make the shield face the opposite side of where the gun is aiming
    //        invisShieldRb.MoveRotation(Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg + 90);
    //        invisShieldRb.MovePosition((Vector2)fakeShield.position + oppositeDirection * 0.5f);
    //    }
    //}
    private void RotateInvisShield()
    {
        if (player != null && gameObject.activeInHierarchy && !playerHealth.IsDead)
        {
            // Get the mouse position in world space
            Vector3 pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointerPos.z = 0f;  // Keep the shield rotation in 2D space (ignore Z-axis)

            // Calculate the direction from the player to the mouse position
            Vector2 direction2 = (pointerPos - player.position).normalized;

            // Calculate the opposite direction for the shield to face
            Vector2 oppositeDirection = -direction2;

            // Set the invisible shield's position relative to the player
            invisShieldRb.transform.position = (Vector2)player.position + oppositeDirection * 0.5f;

            // Rotate the invisible shield
            invisShieldRb.MoveRotation(Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg + 90);
        }
    }
}
