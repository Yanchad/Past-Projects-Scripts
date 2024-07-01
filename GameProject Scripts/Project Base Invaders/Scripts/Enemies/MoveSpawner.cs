using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawner : MonoBehaviour
{

    Alarm alarm;

    
    void Start()
    {
        alarm = FindAnyObjectByType<Alarm>();
    }

    
    void Update()
    {
        if (alarm.greenhouseAlarm.activeInHierarchy)
        {
            transform.position = new Vector3(alarm.greenhouseAlarm.transform.position.x, 12, alarm.greenhouseAlarm.transform.position.z);
        }
        if (alarm.researchLabAlarm.activeInHierarchy)
        {
            transform.position = new Vector3(alarm.researchLabAlarm.transform.position.x, 12, alarm.researchLabAlarm.transform.position.z);
        }
        if (alarm.h3MineAlarm.activeInHierarchy)
        {
            transform.position = new Vector3(alarm.h3MineAlarm.transform.position.x, 12, alarm.h3MineAlarm.transform.position.z);
        }
        if (alarm.mechanicAlarm.activeInHierarchy)
        {
            transform.position = new Vector3(alarm.mechanicAlarm.transform.position.x, 12, alarm.mechanicAlarm.transform.position.z);
        }
    }
}
