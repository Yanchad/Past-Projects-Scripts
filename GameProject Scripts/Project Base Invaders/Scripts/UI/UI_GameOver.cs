using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour, IOnGameOver
{

    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] GameObject gameOverPanel;

    void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        
        RestartGame();
    }

    private void OnEnable()
    {
        playerHealth.RegisterListener(this);
    }
    private void OnDisable()
    {
        playerHealth.RemoveListener(this);
    }
    public void OnGameOver(bool isDead)
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        if (gameOverPanel.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("GameScene");
                Time.timeScale = 1.0f;
            }
        }
    }
}
