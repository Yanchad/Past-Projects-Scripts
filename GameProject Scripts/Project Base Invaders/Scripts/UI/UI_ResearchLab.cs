using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ResearchLab : MonoBehaviour
{
    [SerializeField] private GameObject researchLabWindow;




    public void CloseWindow()
    {
        researchLabWindow.SetActive(false);
    }
}
