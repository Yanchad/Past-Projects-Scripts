using UnityEngine;

public class CodeLockInteract : MonoBehaviour, IInteractable
{

    private bool isSelected;
    private bool isPopped = false;
    public bool IsPopped 
    {
        get
        {
            return isPopped;
        }
        set
        {
            isPopped = value;
        }
    }
    public GameObject popUpUI;

    MouseLook mouseLook;


    private void Start()
    {
        mouseLook = FindObjectOfType<MouseLook>();
    }


    public void Interact()
    {
        isSelected = !isSelected;
        popUpUI.SetActive(true); //Activates PopUpUI
        UnityEngine.Cursor.lockState = CursorLockMode.Confined; //Makes the cursor visible and locked to in-game screen
        UnityEngine.Cursor.visible = true;
        isPopped = true;
        mouseLook.enabled = false;
    }

    public string Look()
    {
        string textToReturn = "Interact";
        if (isPopped == false) return textToReturn;
        else return "";
    }
}
