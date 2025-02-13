using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endTransitionPanel;
    [SerializeField] private float timeTillTransition = 2.1f;
    private bool transitionStarted = false;

    private float timer;

    private void Start()
    {
        transitionStarted = false;
    }
    private void Update()
    {
        if (transitionStarted)
        {
            timer += Time.deltaTime;
            if(timer >= timeTillTransition)
            {
                SceneManager.LoadScene("EndScene");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gate"))
        {
            endTransitionPanel.SetActive(true);
            transitionStarted = true;
        }
    }
}
