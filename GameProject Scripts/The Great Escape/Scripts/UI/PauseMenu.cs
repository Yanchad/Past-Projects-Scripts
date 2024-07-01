using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    BookInteract bookInteract;
    ActivateTimer activateTimer;


    private void Start()
    {
        activateTimer = FindObjectOfType<ActivateTimer>();
        bookInteract = FindObjectOfType<BookInteract>();
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            if (GameIsPaused)
            {                
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (activateTimer.TimerActive == true)
        {
            SpeedrunTimer.Instance.timerText.enabled = true;
            SpeedrunTimer.Instance.watch.Start();
        }
        if (bookInteract.IsPopped == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else 
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        if(activateTimer.TimerActive == true)
        {
            SpeedrunTimer.Instance.timerText.enabled = false;
            SpeedrunTimer.Instance.watch.Stop();
        }
        
    }
    public void LoadMenu()
    {
        SpeedrunTimer.Instance.watch.Reset();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Loading Menu...");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game...");
    }
    public void RestartGame()
    {
        SpeedrunTimer.Instance.watch.Reset();
        SceneManager.LoadScene("MainScene");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Restarting Game...");
    }
}
