using UnityEngine;

public class BookUI : MonoBehaviour
{
    public GameObject BookPopUp;
    MouseLook mouseLook;
    BookInteract bookInteract;


    void Start()
    {
        mouseLook = FindObjectOfType<MouseLook>();
        bookInteract = FindObjectOfType<BookInteract>();
    }

    public void CloseBook()
    {
        BookPopUp.SetActive(false);
        mouseLook.enabled = true;
        bookInteract.IsPopped = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
