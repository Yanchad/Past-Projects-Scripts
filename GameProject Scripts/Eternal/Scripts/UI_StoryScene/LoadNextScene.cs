using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    [SerializeField] private float timeTillTransition;
    private void Start()
    {
        StartCoroutine(waitforAnim());
    }

    private IEnumerator waitforAnim()
    {
        yield return new WaitForSeconds(timeTillTransition);
        SceneManager.LoadScene("MainScene");
    }
}
