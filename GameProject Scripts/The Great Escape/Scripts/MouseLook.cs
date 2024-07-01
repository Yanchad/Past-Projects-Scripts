using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Vector2 mouseMovement;
    [SerializeField] Transform playerRoot;
    [SerializeField] private float sensitivity = 500;
    [SerializeField] private float xRotation;

    

    void Update()
    {
            mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            float horizontalRotation = mouseMovement.x * sensitivity * Time.deltaTime;
            playerRoot.Rotate(Vector3.up * horizontalRotation);

            xRotation -= mouseMovement.y * Time.deltaTime * sensitivity;
            xRotation = Mathf.Clamp(xRotation, -90, 90);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
