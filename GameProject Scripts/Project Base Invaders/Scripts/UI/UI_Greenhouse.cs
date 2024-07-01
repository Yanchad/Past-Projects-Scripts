using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Greenhouse : MonoBehaviour
{
    [SerializeField] private GameObject greenhouseWindow;
    



    public void CloseWindow()
    {
        greenhouseWindow.SetActive(false);
    }
}
