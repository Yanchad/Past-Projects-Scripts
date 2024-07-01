using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsidePlayerCheck : MonoBehaviour
{

    [SerializeField] private GameObject playerGO;

    [SerializeField] private bool isInside;
    public bool IsInside => isInside;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isInside = true;
        }else isInside = false;
    }
}
