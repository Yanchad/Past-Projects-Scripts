using UnityEngine;

public class FlipDesk : MonoBehaviour, IInteractable
{

    Animator animator;
    private bool isSelected;


    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Interact()
    {
        isSelected = !isSelected;
        animator.SetTrigger("DeskFlip");
    }

    public string Look()
    {
        string textToReturn = "Nothing again... (interact)";
        return textToReturn;
    }

}
