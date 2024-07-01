using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class IfObstacle1 : MonoBehaviour
{
    // pelaajan etsiminen eri kohteilta
    // este esim vesi, pelaajan hidastaminen hetkellisesti
    // mahdollisuus johonkin muuhun esteeseen.

    public Animator transition;
    PlayerController1 controller;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController1>();
    }
    IEnumerator LoadLevel(string LevelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(LevelIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            controller.MoveSpeedZ -= 3;
        }
        if (other.gameObject.tag == "Bush")
        {
            controller.MoveSpeedZ -= 4;
        }
        if (other.gameObject.tag == "TheEnd")
        {
            StartCoroutine(LoadLevel("SkeletonShop"));
            
        }
        if (other.gameObject.tag == "TheCustomer")
        {
            StartCoroutine(LoadLevel("SkeletonEndScene"));           
            PlayerPrefs.SetInt("timeValue", 0);
        }       
        if (other.gameObject.tag == "CustomerSlowDown")
        {
            controller.MoveSpeedZ -= 4 * Time.deltaTime;
            controller.MaxSpeed = 4;
        }
    }
}
