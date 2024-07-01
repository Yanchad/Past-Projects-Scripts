using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject controlsTab;
    [SerializeField] private GameObject gameplayTab;
    [SerializeField] private GameObject videoTab;
    [SerializeField] private GameObject audioTab;
    private void Start()
    {
        settings.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Settings()
    {
        settings.SetActive(true);
    }
    public void Exit()
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
        controlsTab.SetActive(false);
        gameplayTab.SetActive(false);
        videoTab.SetActive(true);
        audioTab.SetActive(false);
    }
    public void AudioTabOpen()
    {
        controlsTab.SetActive(false);
        gameplayTab.SetActive(false);
        videoTab.SetActive(false);
        audioTab.SetActive(true);
    }
}
