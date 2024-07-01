using UnityEngine;

public class ResearchLabPopup : MonoBehaviour
{
    GroundCheck groundCheck;
    Controls controls;

    [SerializeField] GameObject researchLabWindow;

    private void Awake()
    {
        groundCheck = FindObjectOfType<GroundCheck>();
        controls = FindObjectOfType<Controls>();
    }
    private void Update()
    {
        if (groundCheck.IsOnResearchLab && controls.IsInteracting)
        {
            //researchLabWindow.SetActive(true);
        }
    }
}
