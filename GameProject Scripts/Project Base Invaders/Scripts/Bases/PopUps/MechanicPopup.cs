using System.Collections;
using UnityEngine;

public class MechanicPopup : MonoBehaviour
{
    GroundCheck groundCheck;
    Controls controls;

    [SerializeField] GameObject mechanicWindow;

    private bool hide;
    public bool Hide { get { return hide; } set { hide = value; } }


    private void Awake()
    {
        groundCheck = FindObjectOfType<GroundCheck>();
        controls = FindObjectOfType<Controls>();
    }
    private void Start()
    {
        hide = true;
    }
    private void Update()
    {
        if (groundCheck.IsOnMechanic && controls.IsInteracting)
        {
            hide = !hide;
        }
        if(groundCheck.IsOnMechanic && controls.IsThrusting)
        {
            hide = true;
        }
        if (!hide)
        {
            mechanicWindow.SetActive(true);
        }
        else
        {
            mechanicWindow.SetActive(false);
        }
    }
}
