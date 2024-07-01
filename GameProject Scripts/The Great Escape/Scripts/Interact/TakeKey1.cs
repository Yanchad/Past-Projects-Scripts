using UnityEngine;


public class TakeKey1 : MonoBehaviour, IInteractable
{

    private bool isSelected;


    public void Interact()
    {
        isSelected = !isSelected;
        if(isSelected) Destroy(gameObject);
    }

    public string Look()
    {
        return "Yummy!";
    }
}
