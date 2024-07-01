using UnityEngine;

public class GreenhousePopup : MonoBehaviour
{
    GroundCheck groundCheck;
    Controls controls;

    [SerializeField] GameObject greenHouseWindow;

    private void Awake()
    {
        groundCheck = FindObjectOfType<GroundCheck>();
        controls = FindObjectOfType<Controls>();
    }
    private void Update()
    {
        if (groundCheck.IsOnGreenhouse && controls.IsInteracting)
        {
            //greenHouseWindow.SetActive(true);
        }
    }
}
