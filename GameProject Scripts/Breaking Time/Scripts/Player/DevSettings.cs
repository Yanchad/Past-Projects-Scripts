using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerKeyBinds))]
public class DevSettings : MonoBehaviour
{
    PlayerKeyBinds playerKeyBinds;

    //[Header("Reset player position (F1: LOAD, F2: SAVE)")]
    //[SerializeField] private Vector3 respawnPosition;
    [Header("Reset player timer Data (F3)")]
    [SerializeField] SaveLoadTimes saveLoadTimes;
    Rigidbody rb;

    

    void Start()
    {
        playerKeyBinds = GetComponent<PlayerKeyBinds>();
        saveLoadTimes = FindObjectOfType<SaveLoadTimes>();
        //rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        //if (Input.GetKeyDown(playerKeyBinds.F1))
        //{
        //    transform.position = respawnPosition;
        //    rb.velocity = Vector3.zero;
        //}
        //if (Input.GetKeyDown(playerKeyBinds.F2)) respawnPosition = transform.position;
        if (Input.GetKeyDown(playerKeyBinds.F3)) saveLoadTimes.ClearSavedData();
    }
}
