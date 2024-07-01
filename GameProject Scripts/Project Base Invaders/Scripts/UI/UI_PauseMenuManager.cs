using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_PauseMenuManager : MonoBehaviour
{
    Controls controls;
    Score score;
    GameStart gameStart;

    [SerializeField] private GameObject playerGO;
    private Rigidbody2D playerRB;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject controlsUI;
    [SerializeField] private GameObject startPopup;
    [SerializeField] private GameObject winGameWindow;
    private bool hide;

    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;

    private void Awake()
    {
        controls = FindObjectOfType<Controls>();
        score = FindObjectOfType<Score>();
        playerRB = playerGO.GetComponent<Rigidbody2D>();
        gameStart = FindObjectOfType<GameStart>();
    }

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        hide = true;
    }

    private void Update()
    {
        if (controls.IsPausing)
        {
            hide = !hide;
        }
        if (hide && gameStart.IsPaused == false && winGameWindow.activeInHierarchy == false)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1;
        }
        else if (!hide)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        hide = true;
        audioSource2.Play();
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        audioSource2.Play();
        SceneManager.LoadScene("GameScene");
    }
    public void MainMenu()
    {
        audioSource2.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        audioSource2.Play();
        Application.Quit();
    }
    public void OpenControls()
    {
        audioSource2.Play();
        controlsUI.SetActive(true);
    }
    public void CloseControls()
    {
        audioSource2.Play();
        controlsUI.SetActive(false);
    }
    public void Unstuck()
    {
        audioSource2.Play();
        playerGO.transform.position = new Vector3(12.5f, 1.25f, playerGO.transform.position.z);
        playerGO.transform.eulerAngles = new Vector3(0, 0, 0);
        playerRB.velocity = new Vector3(0, 0);
        score.Currency -= 200;
    }


    //HOVER SOUND
    public void HoverSound()
    {
        audioSource1.Play();
    }
}
