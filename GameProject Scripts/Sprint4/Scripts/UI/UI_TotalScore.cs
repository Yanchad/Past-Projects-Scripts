using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_TotalScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalStarsText;
    CharacterSelection characterSelection;


    private void Awake()
    {
        UpdateTotalStars();
        characterSelection = FindObjectOfType<CharacterSelection>();
    }
    private void OnEnable()
    {
        characterSelection.totalStarUpdate += UpdateTotalStars;
    }
    private void OnDisable()
    {
        characterSelection.totalStarUpdate -= UpdateTotalStars;
    }

    public void UpdateTotalStars()
    {
        if (PlayerPrefs.HasKey("totalStars"))
        {
            totalStarsText.text = PlayerPrefs.GetInt("totalStars").ToString();
        }
    }
}
