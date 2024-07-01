using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    Transform tf;



    void Start()
    {
        tf = GameObject.FindGameObjectWithTag("ControlsCanvas").GetComponent<Transform>();
        
        tf.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tf.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tf != null) tf.gameObject.SetActive(false);
    }
}
