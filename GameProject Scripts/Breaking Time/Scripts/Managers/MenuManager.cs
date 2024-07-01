using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject controlsTab;
    [SerializeField] private GameObject gameplayTab;
    [SerializeField] private GameObject videoTab;
    [SerializeField] private GameObject audioTab;
    private bool hide;



    private void Start()
    {
        hide = true;
    }
    private void Update()
    {
        OpenMenu();
    }
    private void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            hide = !hide;
        }
        if (hide)
        {
            menu.SetActive(false);
            settings.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!hide)
        {
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } 
    }

    // PauseMenu Navigation
    public void Close()
    {
        hide = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Resume()
    {
        hide = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Settings()
    {
        settings.SetActive(true);
        gameplayTab.SetActive(true);
        controlsTab.SetActive(false);

        videoTab.SetActive(false);
        audioTab.SetActive(false);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitToDesktop()
    {
        Application.Quit();
    }

    // Settings tabs
    public void GameplayTabOpen()
    {
        gameplayTab.SetActive(true);
        controlsTab.SetActive(false);
        videoTab.SetActive(false);
        audioTab.SetActive(false);
    }
    public void CloseSettings()
    {
        settings.SetActive(false);

    }
    public void ControlsTabOpen()
    {
        controlsTab.SetActive(true);
        gameplayTab.SetActive(false);
        videoTab.SetActive(false);
        audioTab.SetActive(false);
    }
    public void VideoTabOpen()
    {
        gameplayTab.SetActive(false);
        controlsTab.SetActive(false);
        videoTab.SetActive(true);
        audioTab.SetActive(false);
    }
    public void AudioTabOpen()
    {
        gameplayTab.SetActive(false);
        controlsTab.SetActive(false);
        videoTab.SetActive(false);
        audioTab.SetActive(true);
    }
}
