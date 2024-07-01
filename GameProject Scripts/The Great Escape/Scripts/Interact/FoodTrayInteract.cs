using UnityEngine;


public class FoodTrayInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        
    }

    public string Look()
    {
        string textToReturn = "tastes like the toilet";
        return textToReturn;
    }
}
