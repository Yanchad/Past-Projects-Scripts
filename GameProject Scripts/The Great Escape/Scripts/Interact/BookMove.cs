using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMove : MonoBehaviour, IInteractable
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
        animator.SetTrigger("BookMove");
    }

    public string Look()
    {
        string textToReturn = "Interact";
        return textToReturn;
    }
}
