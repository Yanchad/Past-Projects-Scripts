using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
    PlayerShoot playerShoot;
    UI_DoorLock doorLock;


    [SerializeField] private InputActionReference interact;
    [SerializeField] private GameObject doorLockPanel;
    private TextMeshProUGUI doorLockText;

    private bool isAtDoor;
    private bool isAtBossDoor;
    private bool bossDoorUnlocked;

    private Animator doorAnimator;
    public bool BossDoorUnlocked { get {  return bossDoorUnlocked; } set { bossDoorUnlocked = value; } }

    private bool setTrigger = false;
    public bool SetTrigger { get { return setTrigger; } set { setTrigger = value; } }

    [Header("Audio")]
    [SerializeField] private GameObject doorOpenSound;

    private void Start()
    {
        playerShoot = GetComponent<PlayerShoot>();
        doorLock = FindObjectOfType<UI_DoorLock>();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        doorAnimator.SetTrigger("DoorOpen");
        if (doorOpenSound != null) Instantiate(doorOpenSound, transform.position, Quaternion.identity);
    }
    private void Interact2(InputAction.CallbackContext context)
    {
        if (!bossDoorUnlocked)
        {
            if(doorLockPanel != null) doorLockPanel.SetActive(true);
        }
        else 
        {
            doorAnimator.SetTrigger("DoorOpen");
        } 
    }

    private void Update()
    {
        if (doorLockPanel.activeInHierarchy)
        {
            playerShoot.enabled = false;
        }
        else playerShoot.enabled = true;

        if (doorLock != null && doorLock.IsUnlocked)
        {
            if (setTrigger)
            {
                doorAnimator.SetTrigger("DoorOpen");
                if (doorOpenSound != null) Instantiate(doorOpenSound, transform.position, Quaternion.identity);
                setTrigger = false;
            }
            //if(doorLockText != null) doorLockText.text = "[Open - E]";
            bossDoorUnlocked = true;
        }
        else
        {
            bossDoorUnlocked = false;
            if (doorLockText != null) doorLockText.text = "[E]";
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Door"))
    //    {
    //        doorAnimator = collision.GetComponent<Animator>();

    //        interact.action.performed += Interact;
    //    }
    //    if (collision.CompareTag("BossDoor"))
    //    {
    //        doorAnimator = collision.GetComponent<Animator>();

    //        interact.action.performed += Interact2;
    //    }
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            doorAnimator = collision.GetComponent<Animator>();

            interact.action.performed += Interact;
        }
        if (collision.CompareTag("BossDoor"))
        {
            if (doorLock != null)
            {
                doorAnimator = collision.GetComponent<Animator>();
                interact.action.performed += Interact2;
                doorLockText = collision.GetComponentInChildren<TextMeshProUGUI>();
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") && collision.isTrigger)
        {
            interact.action.performed -= Interact;
        }
        if (collision.CompareTag("BossDoor") && doorLockPanel != null && collision.isTrigger)
        {
            doorLockPanel.SetActive(false);

            interact.action.performed -= Interact2;
        }
    }
}
