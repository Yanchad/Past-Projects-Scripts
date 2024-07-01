using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_WinGame : MonoBehaviour, IOnGameWin
{
    WinGameTrigger winGameTrigger;
    [SerializeField] MothershipHeart motherShipHeart;

    GameObject winGamePanel;

    private void Awake()
    {
        
    }

    void Start()
    {
        motherShipHeart = GetComponent<MothershipHeart>();
        winGamePanel = Singleton.Instance.WinGamePanelChild;
    }


    private void OnEnable()
    {
        motherShipHeart.RegisterListener(this);
    }
    private void OnDisable()
    {
        motherShipHeart.RemoveListener(this);
    }

    public void OnGameWin(bool hasWon)
    {
        winGamePanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
