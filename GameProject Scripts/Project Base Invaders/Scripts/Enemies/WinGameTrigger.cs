using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameTrigger : MonoBehaviour
{
    MothershipHeart motherShipHeart;


    //private List<IOnGameWin> OnGameWin = new List<IOnGameWin>();
    //private bool hasWon;
    
    //public void RegisterListener(IOnGameWin listener)
    //{
    //    OnGameWin.Add(listener);
    //}
    //public void RemoveListener(IOnGameWin listener)
    //{
    //    OnGameWin.Remove(listener);
    //}
    //public void Invoke(bool hasWon)
    //{
    //    for (int i = 0; i < OnGameWin.Count; i++)
    //    {
    //        OnGameWin[i].OnGameWin(hasWon);
    //    }
    //}

    private void Awake()
    {
        motherShipHeart = GetComponent<MothershipHeart>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //    {
    //        if(motherShipHeart.IsDead == true)
    //        {
    //            hasWon = true;
    //            //Invoke(hasWon);
    //        }
    //    }

    //}
}
