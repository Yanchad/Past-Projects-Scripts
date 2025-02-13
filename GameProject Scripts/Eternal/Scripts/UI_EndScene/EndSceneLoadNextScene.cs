using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneLoadNextScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(loadStartScene());
    }
    private IEnumerator loadStartScene()
    {
        yield return new WaitForSeconds(34.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
