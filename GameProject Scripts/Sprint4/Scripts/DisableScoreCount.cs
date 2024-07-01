using UnityEngine;


public class DisableScoreCount : MonoBehaviour
{
    ScoreManager scoreManager;
    void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
        scoreManager.enabled = false;
    }
}
