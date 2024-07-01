using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ShowEndScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreManager scoreManager;

    
    void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
        int stars = PlayerPrefs.GetInt("stars");
        scoreText.text = ("Delivery Rating: " + stars.ToString() + "/5");
        
        if (PlayerPrefs.HasKey("totalStars"))
        {
            stars += PlayerPrefs.GetInt("totalStars");
        }
        PlayerPrefs.SetInt("totalStars", stars);
        Time.timeScale = 0f;
    }
}
