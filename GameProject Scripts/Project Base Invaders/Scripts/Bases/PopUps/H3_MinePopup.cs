using UnityEngine;

public class H3_MinePopup : MonoBehaviour
{
    GroundCheck groundCheck;
    Controls controls;

    [SerializeField] GameObject h3_MineWindow;

    private void Awake()
    {
        groundCheck = FindObjectOfType<GroundCheck>();
        controls = FindObjectOfType<Controls>();
    }
    private void Update()
    {
        if (groundCheck.IsOnH3Mine && controls.IsInteracting)
        {
            //h3_MineWindow.SetActive(true);
        }
    }
}
