using System.Collections.Generic;
using UnityEngine;


public class PlayerInteract : MonoBehaviour
{

    PauseMenu pauseMenu;

    GameObject lookedObject;

    private string lookText;
    public string LookText => lookText;

    [SerializeField] private Transform cameraTF;
    [SerializeField] private float rayLength = 4;
    [SerializeField] public List<string> inventory = new List<string>();


    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        Raycast();
    }


    public void Raycast()
    {
        if (Physics.Raycast(cameraTF.position, cameraTF.forward, out RaycastHit hitInfo, rayLength))
        {
            if (lookedObject != hitInfo.collider.gameObject) lookText = "";

            IInteractable[] interactables = hitInfo.collider.gameObject.GetComponents<IInteractable>(); // If Raycast hits an object, it tries to find Multiple Components of IInteractable
            foreach (IInteractable interactable in interactables) // Displays a text upon looking an item with an IInteractable script attached to it
            {
                if (Time.timeScale == 0) lookText = "";
                else lookText = interactable.Look();
            }
            

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0)) // Interacting with an object with "E" key or left mouse button
            {
                if (interactables.Length > 0)
                {
                    if (hitInfo.collider.gameObject.tag == "Item" && !inventory.Contains(hitInfo.collider.gameObject.name)) // If Inventory list doesn't contain a name of an item add object name to list
                    {
                        inventory.Add(hitInfo.collider.gameObject.name);
                    }
                    foreach (IInteractable interactable in interactables)
                    {
                        interactable.Interact();
                    }
                }
            }
        }
        else lookText = "";
    }
    public bool FindInteraction(string name)
    {
        if (inventory.Contains(name)) return true; // if inventory list ha a certain name, bool return true
        return false;                              // else return false
    }
}
