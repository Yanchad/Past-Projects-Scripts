using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHint : MonoBehaviour
{
    [SerializeField] private GameObject hint1;
    [SerializeField] private GameObject hint2;
    [SerializeField] private GameObject hint3;
    [SerializeField] private GameObject hint4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hint1"))
        {
            hint1.SetActive(true);
            Destroy(collision.gameObject);
            StartCoroutine(waitSeconds());
        }
        if (collision.CompareTag("Hint2"))
        {
            hint2.SetActive(true);
            Destroy(collision.gameObject);
            StartCoroutine(waitSeconds());
        }
        if (collision.CompareTag("Hint3"))
        {
            hint3.SetActive(true);
            Destroy(collision.gameObject);
            StartCoroutine(waitSeconds());
        }
        if (collision.CompareTag("Hint4"))
        {
            hint4.SetActive(true);
            Destroy(collision.gameObject);
            StartCoroutine(waitSeconds());
        }
    }

    private IEnumerator waitSeconds()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
    }
}
