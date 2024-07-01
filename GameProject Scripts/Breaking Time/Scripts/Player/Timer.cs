using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private SaveLoadTimes saveLoadTimes;
    [Header("Read Only")]
    [SerializeField] private float totalTimer;
    [SerializeField] private float checkPointTimer;
    private bool checkPointHit;
    private bool isRunning = true;
    private bool finishLineHit;
    [SerializeField] private List<float> timerList = new List<float>();

    public event Action<float, bool, float> OnCheckpointReached;
    public event Action<float, bool, float> OnFinishLineCrossed;

    // Getters
    public float CheckPointTimer => checkPointTimer;
    public float TotalTimer => totalTimer;
    public bool CheckPointHit => checkPointHit;
    public bool FinishLineHit => finishLineHit;
    public bool IsRunning => isRunning;
    public List<float> TimerList => timerList;


    private void Start()
    {
        saveLoadTimes = FindObjectOfType<SaveLoadTimes>();

        finishLineHit = false;
    }
    void Update()
    {
        if (isRunning) ActiveTimer();
    }


    private void ActiveTimer()
    {
         checkPointTimer += Time.deltaTime;
         totalTimer += Time.deltaTime;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            checkPointHit = true;
            timerList.Add(checkPointTimer);
            saveLoadTimes.SaveData(checkPointTimer, collision.gameObject.name, out bool isNewRecord, out float timeDifference);
            OnCheckpointReached?.Invoke(checkPointTimer, isNewRecord, timeDifference);
            checkPointTimer = 0f;
            collision.gameObject.SetActive(false);
        }

        else if(collision.gameObject.tag == "FinishLine")
        {
            isRunning = false;
            finishLineHit = true;
            timerList.Add(checkPointTimer);
            saveLoadTimes.SaveData(totalTimer, collision.gameObject.name, out  bool isNewRecord , out float timeDifference);
            OnCheckpointReached?.Invoke(checkPointTimer, isNewRecord, timeDifference);
            OnFinishLineCrossed?.Invoke(totalTimer, isNewRecord, timeDifference);
            collision.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Checkpoint") checkPointHit = false;
    }
}
