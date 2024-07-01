using UnityEngine;


public class TakeKey2 : MonoBehaviour, IInteractable
{
    private bool isSelected;


    public void Interact()
    {
        isSelected = !isSelected;
        if(isSelected) Destroy(gameObject);
    }

    public string Look()
    {
        return "Must've been the wind!";
    }
}
