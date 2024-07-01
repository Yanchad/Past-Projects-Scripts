using UnityEngine;


public class OpenDoorWithCode : MonoBehaviour, IInteractable
{
    KeyPad keyPad;
    Animator animator;

    private float timer;
    private bool isSelected;
    private bool isOpen;

    void Start()
    {
        keyPad = FindObjectOfType<KeyPad>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }


    public void Interact()
    {
        isSelected = !isSelected;
        if(keyPad.Unlocked == true && isSelected && isOpen == false)
        {
            animator.SetTrigger("OpenDoor");
            isOpen = true;
            timer = 0f;
        }
        else if (timer > 0.3f && isOpen == true)
        {
            animator.SetTrigger("CloseDoor");
            isOpen = false;
            timer = 0f;
        }
    }

    public string Look()
    {
        string textToReturn = "Enter a correct code";
        if(keyPad.Unlocked == true && isOpen == false)
        {
            textToReturn = "Open";
        }
        else if (isOpen == true)
        {
            textToReturn = "Close";
        }
        return textToReturn;
    }
}
