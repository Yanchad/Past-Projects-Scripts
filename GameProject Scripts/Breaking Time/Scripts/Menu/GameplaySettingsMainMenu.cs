using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySettingsMainMenu : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private Toggle tutorialToggleBtn;

    void Start()
    {
        // Load the saved toggle state from PlayerPrefs
        if (PlayerPrefs.HasKey("ToggleState"))
        {
            bool savedToggleState = PlayerPrefs.GetInt("ToggleState") == 1;

            // Set the toggle button's state based on the saved state
            tutorialToggleBtn.isOn = savedToggleState;
        }
        else
        {
            // Default behaviour: toggle on
            tutorialToggleBtn.isOn = true;
        }
    }

    
    public void ToggleValueMainMenu()
    {
        // Toggle the boolean value
        bool currentState = tutorialToggleBtn.isOn;

        // Save the current toggle state to PlayerPrefs
        PlayerPrefs.SetInt("ToggleState", currentState ? 1 : 0);
        PlayerPrefs.Save();
    }
}
