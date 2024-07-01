using UnityEngine;


public class OpenDoorWithKey3 : MonoBehaviour, IInteractable
{

    PlayerInteract playerInteract;
    Animator animator;

    private bool isSelected;
    private float timer;
    
    [SerializeField] private bool isOpen;
    [SerializeField] private string keyName = "Key3";


    void Start()
    {
        playerInteract = FindObjectOfType<PlayerInteract>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }


    public void Interact()
    {
        isSelected = !isSelected;
        if(playerInteract.inventory.Contains("Key3") && isSelected && isOpen == false)
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
        string textToReturn = "It's way too windy in some rooms, you need a key for this!";
        if (playerInteract.FindInteraction(keyName))
        {
            if(isOpen == false) textToReturn = "Open";
            else if(isOpen == true)
            {
                textToReturn = "Close";
            }
        }
        return textToReturn;
    }
}
