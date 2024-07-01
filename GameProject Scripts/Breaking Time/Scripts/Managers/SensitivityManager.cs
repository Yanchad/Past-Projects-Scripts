using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityManager : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensNumberTxt;

    [SerializeField] private float sensitivity = 50f;
    [Tooltip("Default sensitivity value, (Reset To Default)")]
    [SerializeField] private float defaultSensitivity = 50f;
    public float Sensitivity => sensitivity;

    private string sensitivityKey = "MouseSensitivity";

    void Start()
    {
        // Load Sensitivity from PlayerPrefs or set default if not available
        sensitivity = PlayerPrefs.GetFloat(sensitivityKey, sensitivity);

        // Initialize slider value
        sensitivitySlider.value = sensitivity;
    }

    private void OnEnable()
    {
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }
    private void OnDisable()
    {
        sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
    }
    public void OnSensitivityChanged(float newSensitivity)
    {
        // Update sensitivity value
        sensitivity = newSensitivity;

        // Save sensitivity to PlayerPrefs
        PlayerPrefs.SetFloat(sensitivityKey, sensitivity);
        PlayerPrefs.Save();

        sensNumberTxt.text = sensitivity.ToString("F0");
    }
    public void ResetSensToDefault()
    {
        sensitivity = defaultSensitivity;

        PlayerPrefs.SetFloat(sensitivityKey, sensitivity);
        PlayerPrefs.Save();
        sensNumberTxt.text = sensitivity.ToString("F0");
        sensitivitySlider.value = sensitivity;
    }
}
