using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOver : MonoBehaviour, IOnGameOver
{
    [SerializeField] private GameObject deathScreenPanel;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float timerBeforePause;

    private void Awake()
    {
        deathScreenPanel.SetActive(false);
    }
    private void OnEnable()
    {
        playerHealth.RegisterDeathListener(this);
    }
    private void OnDisable()
    {
        playerHealth.RemoveDeathListener(this);
    }


    public void OnGameOver(bool isDead)
    {
        deathScreenPanel.SetActive(true);
        StartCoroutine(WaitBeforePause());
    }
    private IEnumerator WaitBeforePause()
    {
        yield return new WaitForSeconds(timerBeforePause);
        Time.timeScale = 0f;
    }
}
