using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float timeToWait = 0.2f; // Delay before scene loads
    private float timer = 0f;
    private bool isTransitioning = false;

    public void StartGame()
    {
        // Start the transition when the player presses the button
        animator.SetTrigger("Start");
        isTransitioning = true;  // Set flag to indicate transition has started
    }

    void Update()
    {
        // Only update the timer if the transition is active
        if (isTransitioning)
        {
            timer += Time.deltaTime;

            // When the timer exceeds the set delay, load the next scene
            if (timer >= timeToWait)
            {
                SceneManager.LoadScene("StoryScene");
            }
        }
    }
}
