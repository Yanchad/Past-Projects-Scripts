using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    GroundCheck groundCheck;

    [SerializeField] private bool isThrusting;
    [SerializeField] private bool isRotatingLeft;
    [SerializeField] private bool isRotatingRight;
    [SerializeField] private bool isShooting;
    [SerializeField] private bool isShielding;
    private bool isInteracting;
    private bool isRefueling;
    private bool isPausing;
    private bool isRepairing;
    private bool f1;
    [SerializeField]private bool useAbility1;
    

    public bool IsThrusting => isThrusting;
    public bool IsRotatingLeft => isRotatingLeft;
    public bool IsRotatingRight => isRotatingRight;
    public bool IsShooting => isShooting;
    public bool IsShielding => isShielding;
    public bool IsInteracting => isInteracting;
    public bool IsRefueling => isRefueling;
    public bool IsPausing => isPausing;
    public bool IsRepairing => isRepairing;
    public bool UseAbility1 => useAbility1;
    public bool F1 => f1;

    void Awake()
    {
        groundCheck = GetComponent<GroundCheck>();
    }
    private void Start()
    {
        isThrusting = false;
        isRotatingLeft = false;
        isRotatingRight = false;
        isShooting = false;
        isShielding = false;
        useAbility1 = false;
        f1 = false;
    }


    void Update()
    {
        PlayerControls();
        BaseControls();
        UI();
        AbilityControls();
        DevKeys();
    }

    private void PlayerControls()
    {
        //Rotating
        if (Input.GetKey(KeyCode.A))
        {
            isRotatingLeft = true;
        }
        else isRotatingLeft = false;

        if (Input.GetKey(KeyCode.D))
        {
            isRotatingRight = true;
        }
        else isRotatingRight = false;

        //Thruster
        if (Input.GetKey(KeyCode.Space))
        {
            isThrusting = true;
        }
        else isThrusting = false;

        //Shooting
        if (Input.GetMouseButton(0))
        {
            isShooting = true;
        }
        else isShooting = false;

        //Shielding
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isShielding = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            isShielding = false;
        }
    }
    private void AbilityControls()
    {
        //Abilities
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            useAbility1 = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            useAbility1 = false;
        }
    }

    private void BaseControls()
    {
        //Interacting
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = true;
        }else isInteracting = false;

        //Repair
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRepairing = true;
        }else isRepairing = false;

        //Refuel
        if (Input.GetKeyDown(KeyCode.F))
        {
            isRefueling = true;
        }else isRefueling = false;
    }

    private void UI()
    {
        //Pausing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPausing = true;
        }
        else isPausing = false;
    }

    private void DevKeys()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            f1 = true;
        } else f1 = false;
    }
}
