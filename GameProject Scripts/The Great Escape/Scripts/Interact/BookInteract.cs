using UnityEngine;

public class BookInteract : MonoBehaviour, IInteractable
{

    public GameObject BookPopUp;
    MouseLook mouseLook;
    public GameObject ClosedBook;
    public GameObject OpenBook;

    private bool isPopped;
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

    private bool isSelected;


    private void Start()
    {
        mouseLook = FindObjectOfType<MouseLook>();
        OpenBook.SetActive(false);
        ClosedBook.SetActive(true);
    }


    public void Interact()
    {
        isSelected = !isSelected;
        BookPopUp.SetActive(true);
        mouseLook.enabled = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = true;
        isPopped = true;
        OpenBook.SetActive(true);
        ClosedBook.SetActive(false);
    }

    public string Look()
    {
        string textToReturn = "Read";
        if (isPopped)
        {
            textToReturn = "";
        }

        return textToReturn;
    }
}
