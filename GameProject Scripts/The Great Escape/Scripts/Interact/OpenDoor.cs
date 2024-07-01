using UnityEngine;


public class OpenDoor : MonoBehaviour, IInteractable
{
    Animator animator;

    private bool isSelected;
    private float timer;

    [SerializeField] private bool isOpen;



    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    
    void Update()
    {
        timer += Time.deltaTime;
    }


    public void Interact()
    {
        isSelected = !isSelected;
        if (isSelected && isOpen == false)
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
        string textToReturn = "Open";
            if (isOpen == false) textToReturn = "Open";
            else if (isOpen == true)
            {
                textToReturn = "Close";
            }
        return textToReturn;
    }
}
