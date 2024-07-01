using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] Animator panelWhiteAnimator;
    [SerializeField] Animator panelBlackAnimator;




    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void HowToPlay()
    {
        howToPlayPanel.SetActive(true);
        if(panelWhiteAnimator != null && panelWhiteAnimator.isActiveAndEnabled)
        {
            panelWhiteAnimator.SetTrigger("isActive");
        }
        if (panelBlackAnimator != null && panelBlackAnimator.isActiveAndEnabled)
        {
            panelBlackAnimator.SetTrigger("isActiveBlack");
        }
        
    }
    public void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }
}
