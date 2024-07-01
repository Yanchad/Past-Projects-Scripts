using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowToggle : MonoBehaviour
{

    ShadowCaster2D shadowCaster;
    
    void Start()
    {
        shadowCaster = GetComponent<ShadowCaster2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
            shadowCaster.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            shadowCaster.enabled = true;
        }
    }
}
