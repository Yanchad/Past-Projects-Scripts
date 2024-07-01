using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_H3Mine : MonoBehaviour
{
    [SerializeField] private GameObject h3MineWindow;




    public void CloseWindow()
    {
        h3MineWindow.SetActive(false);
    }
}
