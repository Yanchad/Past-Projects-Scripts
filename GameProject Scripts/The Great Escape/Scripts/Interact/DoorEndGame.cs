using UnityEngine;


public class DoorEndGame : MonoBehaviour, IInteractable
{

    PlayerInteract playerInteract;
    MouseLook mouseLook;

    private bool isSelected;

    [SerializeField] private GameObject lookText;
    [SerializeField] private string keyName = "LastKey";
    [SerializeField] private GameObject endGameUI;
    

    void Start()
    {
        playerInteract = FindObjectOfType<PlayerInteract>();
        mouseLook = FindObjectOfType<MouseLook>();
    }


    public void Interact()
    {
        isSelected = !isSelected;
        if(playerInteract.inventory.Contains("LastKey") && isSelected)
        {
            playerInteract.enabled = false;
            endGameUI.SetActive(true);
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            UnityEngine.Cursor.visible = true;
            mouseLook.enabled = false;
            Time.timeScale = 0f;
            SpeedrunTimer.Instance.watch.Stop();
            lookText.SetActive(false);
            SpeedrunTimer.Instance.timerText.enabled = false;
        }
        else
        {
            lookText.SetActive(true);
            endGameUI.SetActive(false);
            mouseLook.enabled = true;
            Time.timeScale = 1f;
        }
    }

    public string Look()
    {
        string textToReturn = "";
        if (Time.timeScale == 1)
        {
            if (playerInteract.FindInteraction(keyName))
            {
                textToReturn = "Open";
            }
            else textToReturn = "Hope it's not windy outside, I need a key for this.";
        }
        else textToReturn = "";
        return textToReturn;
    }
}
