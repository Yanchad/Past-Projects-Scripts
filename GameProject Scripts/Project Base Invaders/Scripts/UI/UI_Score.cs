using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] Score score;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreTxt;
    [SerializeField] private TextMeshProUGUI gameWinScoreTxt;
    [SerializeField] private TextMeshProUGUI creditsTxt;


    void Start()
    {
        
    }

    
    void Update()
    {
        scoreText.text = "SCORE: " + score.PlayerScore.ToString();
        creditsTxt.text = "CREDITS: " + score.Currency.ToString("0");
        gameOverScoreTxt.text = "SCORE: " + score.PlayerScore.ToString();
        gameWinScoreTxt.text = "SCORE: " + score.PlayerScore.ToString();
    }
}
