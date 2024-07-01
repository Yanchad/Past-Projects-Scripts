using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsVideo : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private GameObject confirmationPopup;
    [SerializeField] private TextMeshProUGUI timerTxt;
    private Resolution[] resolutions;

    public Toggle fullscreenTog, vsyncTog;
    private int selectedResolutionIndex;

    private const string SelectedResolutionIndexKey = "SelectedResolutionIndex";
    private const string FullscreenToggleKey = "FullscreenToggle";
    private const string VsyncToggleKey = "VsyncToggle";

    private bool changesMade = false; // Flag to track changes
    [Header("Confirmation Timer")]
    [Tooltip("Time to Cancel Confirmation window")]
    [SerializeField] private float timeToCancel = 5f;
    [Header("Read Only")]
    [SerializeField] private float timer;
    private bool confirmationIsActive;

    // Variables to store previous settings
    private bool prevFullscreenState;
    private bool prevVsyncState;
    private int prevResolutionIndex;



    void Start()
    {
        // if togglekey exists and is 1, toggle is true and 0, toggle is false else fullscreen is set to default fullscreen.
        if (PlayerPrefs.HasKey(FullscreenToggleKey))
        {
            fullscreenTog.isOn = PlayerPrefs.GetInt(FullscreenToggleKey) == 1;
        }
        else fullscreenTog.isOn = Screen.fullScreen;

        // Same as above with VSync
        if (PlayerPrefs.HasKey(VsyncToggleKey))
        {
            vsyncTog.isOn = PlayerPrefs.GetInt(VsyncToggleKey) == 1;
        }else vsyncTog.isOn = QualitySettings.vSyncCount != 0;

        GetAvailableResolutions();
        
        // Store initial settings
        prevFullscreenState = fullscreenTog.isOn;
        prevVsyncState = vsyncTog.isOn;
        prevResolutionIndex = selectedResolutionIndex;

        // Set the selected resolution index based on PlayerPrefs or current resolution
        if (PlayerPrefs.HasKey(SelectedResolutionIndexKey))
        {
            selectedResolutionIndex = PlayerPrefs.GetInt(SelectedResolutionIndexKey, Screen.currentResolution.width * Screen.currentResolution.height);
        }
        else
        {
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    selectedResolutionIndex = i;
                    break;
                }
            }
        }
        resolutionDropdown.value = selectedResolutionIndex;


        timer = timeToCancel;
    }
    private void Update()
    {
        // Timer for Confirmation Window
        if (confirmationPopup.activeInHierarchy)
        {
            confirmationIsActive = true;
        }else confirmationIsActive = false;

        if (confirmationIsActive)
        {
            timer -= Time.deltaTime;
            timerTxt.enabled = true;
            timerTxt.text = timer.ToString("F2");
            if (timer <= 0)
            {
                CancelChanges();
                timer = timeToCancel;
                confirmationPopup.SetActive(false);
                timerTxt.enabled = false;
            }
        }
    }


    private void GetAvailableResolutions()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRateRatio + "hz ";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);

        if (PlayerPrefs.HasKey(SelectedResolutionIndexKey)) selectedResolutionIndex = PlayerPrefs.GetInt(SelectedResolutionIndexKey, currentResolutionIndex);
        resolutionDropdown.value = selectedResolutionIndex;
    }

    // Set the selected resolution index without applying it immediately
    public void SetSelectedResolution(int index)
    {
        selectedResolutionIndex = index;
        changesMade = true;
    }

    private void SetAvailableResolutions(int index)
    {
        FullScreenMode fullscreenMode = fullscreenTog.isOn ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;

        Screen.SetResolution(resolutions[index].width, resolutions[index].height, fullscreenMode, resolutions[index].refreshRateRatio);
        Screen.fullScreen = fullscreenTog.isOn;

        Debug.Log("Resolution applied: " + resolutions[index].width + "x" + resolutions[index].height);
    }
    


    // Applying and saving Video Settings
    private void ApplyVideoSettings()
    {
        SetAvailableResolutions(selectedResolutionIndex);

        Screen.fullScreen = fullscreenTog.isOn;
        QualitySettings.vSyncCount = vsyncTog.isOn ? 1 : 0;
    }

    public void ApplyChanges()
    {
        ApplyVideoSettings();
        confirmationPopup.SetActive(true);
    }

    public void SaveChanges()
    {
        if (changesMade)
        {
            PlayerPrefs.SetInt(SelectedResolutionIndexKey, selectedResolutionIndex);
            PlayerPrefs.SetInt(FullscreenToggleKey, fullscreenTog.isOn ? 1 : 0);
            PlayerPrefs.SetInt(VsyncToggleKey, vsyncTog.isOn ? 1 : 0);
            PlayerPrefs.Save();

            // Update previous settings
            prevFullscreenState = fullscreenTog.isOn;
            prevVsyncState = vsyncTog.isOn;
            prevResolutionIndex = selectedResolutionIndex;

            changesMade = false;
        }
        timer = timeToCancel;
        confirmationPopup.SetActive(false);
    }

    public void CancelChanges()
    {
        if (changesMade)
        {
            // Revert to previous settings
            fullscreenTog.isOn = prevFullscreenState;
            vsyncTog.isOn = prevVsyncState;
            selectedResolutionIndex = prevResolutionIndex;
            ApplyVideoSettings();

            PlayerPrefs.SetInt(SelectedResolutionIndexKey, selectedResolutionIndex);
            PlayerPrefs.SetInt(FullscreenToggleKey, prevFullscreenState ? 1 : 0);
            PlayerPrefs.SetInt(VsyncToggleKey, prevVsyncState ? 1 : 0);
            PlayerPrefs.Save();

            changesMade = false;
        }
        GetAvailableResolutions();
        confirmationPopup.SetActive(false);
    }
}