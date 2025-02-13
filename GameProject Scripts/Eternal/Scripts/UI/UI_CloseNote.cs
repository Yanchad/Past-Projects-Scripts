using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CloseNote : MonoBehaviour
{
    [SerializeField] private GameObject note1;
    [SerializeField] private GameObject note2;
    [SerializeField] private GameObject note3;
    [SerializeField] private GameObject note4;



    public void CloseNotes()
    {
        if(note1 != null && note2 != null && note3 != null && note4 != null)
        {
            if (note1.activeInHierarchy)
            {
                note1.SetActive(false);
            }
            if (note2.activeInHierarchy)
            {
                note2.SetActive(false);
            }
            if (note3.activeInHierarchy)
            {
                note3.SetActive(false);
            }
            if (note4.activeInHierarchy)
            {
                note4.SetActive(false);
            }
            Time.timeScale = 1.0f;
        }
    }
}
