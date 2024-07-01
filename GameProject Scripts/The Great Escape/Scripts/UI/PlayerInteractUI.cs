using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{

    TextMeshProUGUI lookText;
    PlayerInteract playerInteract;


    void Start()
    {
        playerInteract = FindObjectOfType<PlayerInteract>();
        lookText = GetComponent<TextMeshProUGUI>();
        lookText.text = "";
    }

    void Update()
    {
        lookText.text = playerInteract.LookText;
    }
}
