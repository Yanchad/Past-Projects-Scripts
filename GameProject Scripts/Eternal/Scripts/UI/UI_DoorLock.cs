using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_DoorLock : MonoBehaviour
{
    [SerializeField] OpenDoor playerOpenDoor;
    [SerializeField] private GameObject doorLockPanel;
    [SerializeField] private TextMeshProUGUI codeText;
    

    private string codeTextValue = "";

    private bool isUnlocked;
    public bool IsUnlocked => isUnlocked;

    private void Start()
    {
        isUnlocked = false;
    }

    private void Update()
    {
        codeText.text = codeTextValue;

        if(codeTextValue == "SOUL")
        {
            isUnlocked = true;
        }
        if(codeTextValue.Length >= 10)
        {
            codeTextValue = "";
        }
    }
    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }
    public void ClearDigits()
    {
        codeTextValue = "";
    }
    public void Confirm()
    {
        if(isUnlocked)
        {
            doorLockPanel.SetActive(false);
            playerOpenDoor.SetTrigger = true;
        }
        if (!IsUnlocked)
        {
            codeTextValue = "";
        }
    }
    public void ClosePanel()
    {
        codeTextValue = ""; 
        doorLockPanel.SetActive(false);
        
    }
}
