using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DoorInteract : MonoBehaviour
{
    [SerializeField] private GameObject doorTextCanvas;
    [SerializeField] private GameObject greenLight1;
    [SerializeField] private GameObject greenLight2;

    private bool isOpen = false;

    private void Start()
    {
        isOpen = false;
    }

    private void Update()
    {
        if (greenLight1.activeInHierarchy && greenLight2.activeInHierarchy)
        {
            doorTextCanvas.SetActive(false);
            isOpen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!isOpen) doorTextCanvas.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!isOpen) doorTextCanvas.SetActive(false);
        }
    }
}
