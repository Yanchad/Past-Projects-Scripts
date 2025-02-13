using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Shortcuts : MonoBehaviour
{
    [SerializeField] private InputActionReference restartGame;

    private void OnEnable()
    {
        restartGame.action.performed += Restart;
    }
    private void OnDisable()
    {
        restartGame.action.performed -= Restart;
    }

    private void Restart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }

}
