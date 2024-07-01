using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySettings : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private Toggle tutorialToggleBtn;
    [SerializeField] private List<GameObject> tutorialTexts = new List<GameObject>();


    void Start()
    {
        if (tutorialTexts != null)
        {
            if (PlayerPrefs.HasKey("ToggleState"))
            {
                // Load the saved toggle state from PlayerPrefs
                bool savedToggleState = PlayerPrefs.GetInt("ToggleState") == 1;

                // Set the toggle button's state based on the saved state
                tutorialToggleBtn.isOn = savedToggleState;

                if (tutorialTexts != null)
                {

                    // Set the visibility of all objects based on the saved state
                    foreach (GameObject obj in tutorialTexts)
                    {
                        obj.SetActive(savedToggleState);
                    }
                }
            }
            else
            {
                // Default behavior: all objects visible and toggle on

                foreach (GameObject obj in tutorialTexts)
                {
                    obj.SetActive(true);
                }

                tutorialToggleBtn.isOn = true;
            }
        }
        else
        {
            Debug.LogError("tutorialTexts is not assigned! Please assign the turorial text objects in the Unity Editor");
        }
    }
    public void ToggleTutorialVisibility()
    {
        if (tutorialTexts != null)
        {
            // Toggle the visibility of all objects
            bool currentState = tutorialTexts[0].activeSelf;  // Use the first object to determine the current state
            foreach (GameObject obj in tutorialTexts)
            {
                obj.SetActive(!currentState);
            }

            // Toggle the toggle button state
            tutorialToggleBtn.isOn = !currentState;

            // Save the current toggle state to PlayerPrefs
            PlayerPrefs.SetInt("ToggleState", currentState ? 0 : 1);
            PlayerPrefs.Save(); // Save PlayerPrefs
        }
        else
        {
            Debug.LogError("tutorialTexts is not assigned! Please assign the turorial text objects in the Unity Editor");
        }
    }
}
