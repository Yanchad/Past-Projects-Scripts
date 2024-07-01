using UnityEngine;


public class TakeKey : MonoBehaviour, IInteractable
{

    private bool isSelected;
    

    public void Interact()
    {
        isSelected = !isSelected;
        if(isSelected) Destroy(gameObject);
    }

    public string Look()
    {
        return "A key it seems!";
    }
}
