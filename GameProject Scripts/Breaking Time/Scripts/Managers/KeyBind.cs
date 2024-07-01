using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBind : MonoBehaviour
{
    
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    [SerializeField] private TextMeshProUGUI forward, left, backward, right, jump, crouchSlide, glide, grapple;
    private GameObject currentKey;

    private Color32 normal = new Color32(255, 255, 255, 255);
    private Color32 selected = new Color32(22, 255, 0, 255);


    void Start()
    {
        LoadKeys();
    }


    void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if (e.isMouse)
            {
                switch (e.button)
                {
                    case 0:
                        keys[currentKey.name] = KeyCode.Mouse0;
                        currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Mouse 1";
                        currentKey.GetComponent<Image>().color = normal;
                        break;
                    case 1:
                        keys[currentKey.name] = KeyCode.Mouse1;
                        currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Mouse 2";
                        currentKey.GetComponent<Image>().color = normal;
                        break;
                    case 2:
                        keys[currentKey.name] = KeyCode.Mouse2;
                        currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Mouse 3";
                        currentKey.GetComponent<Image>().color = normal;
                        break;
                    case 3:
                        keys[currentKey.name] = KeyCode.Mouse3;
                        currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Mouse 4";
                        currentKey.GetComponent<Image>().color = normal;
                        break;
                    case 4:
                        keys[currentKey.name] = KeyCode.Mouse4;
                        currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Mouse 5";
                        currentKey.GetComponent<Image>().color = normal;
                        break;
                }
                currentKey = null;
            }   
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        if(currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }
    public void SaveKeys()
    {
        foreach(var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
    public void LoadKeys()
    {

        keys.Add("ForwardBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ForwardBtn", "W")));
        keys.Add("LeftBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftBtn", "A")));
        keys.Add("BackwardBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BackwardBtn", "S")));
        keys.Add("RightBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightBtn", "D")));
        keys.Add("JumpBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpBtn", "Space")));
        keys.Add("CrouchSlideBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CrouchSlideBtn", "LeftControl")));
        keys.Add("GlideBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GlideBtn", "LeftShift")));
        keys.Add("GrappleBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GrappleBtn", "Mouse0")));


        UpdateUI();
    }
    private void UpdateUI()
    {
        forward.text = keys["ForwardBtn"].ToString();
        left.text = keys["LeftBtn"].ToString();
        backward.text = keys["BackwardBtn"].ToString();
        right.text = keys["RightBtn"].ToString();
        jump.text = keys["JumpBtn"].ToString();
        crouchSlide.text = keys["CrouchSlideBtn"].ToString();
        glide.text = keys["GlideBtn"].ToString();
        grapple.text = keys["GrappleBtn"].ToString();
    }

    // Reset to Default
    public void ResetToDefault()
    {
        // Clear existing keys dictionary
        keys.Clear();

        // Load default keys
        SetDefaultKeys();

        // Update UI text with default key bindings
        UpdateUI();
    }
    private void SetDefaultKeys()
    {
        keys.Add("ForwardBtn", KeyCode.W);
        keys.Add("LeftBtn", KeyCode.A);
        keys.Add("BackwardBtn", KeyCode.S);
        keys.Add("RightBtn", KeyCode.D);
        keys.Add("JumpBtn", KeyCode.Space);
        keys.Add("CrouchSlideBtn", KeyCode.LeftControl);
        keys.Add("GlideBtn", KeyCode.LeftShift);
        keys.Add("GrappleBtn", KeyCode.Mouse0);


        SaveKeys();
    }
}
