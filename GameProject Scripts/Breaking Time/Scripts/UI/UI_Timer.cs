using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private Timer playerTimer;
    [SerializeField] private SaveLoadTimes saveLoadTimes;
    
    [SerializeField] private TextMeshProUGUI totalTimerTxt;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextMeshProUGUI checkpointTimesTxt;

    [SerializeField] private TextMeshProUGUI finishTimeText;
    [SerializeField] private TextMeshProUGUI recordTimeText;
    [SerializeField] private TextMeshProUGUI yourTotalTimeText;

    [SerializeField] private InitializeTimeBox initializeTimeBox;
    [SerializeField] private Transform CheckpointArea;

    private string newRecord = "NEW RECORD!";
    private string yourTime = "FINISHING TIME:";

    private void Start()
    {
        finishTimeText.enabled = false;
        yourTotalTimeText.enabled = false;
        recordTimeText.enabled = false;
    }

    private void OnEnable()
    {
        playerTimer.OnCheckpointReached += UpdateUIText;
        playerTimer.OnFinishLineCrossed += UpdateFinishUIText;
    }

    private void OnDisable()
    {
        playerTimer.OnCheckpointReached -= UpdateUIText;
        playerTimer.OnFinishLineCrossed -= UpdateFinishUIText;
    }
    private void Update()
    {
        if (timerTxt != null && playerTimer.IsRunning)
        {
            timerTxt.text = playerTimer.CheckPointTimer.ToString("F2");
        }
        if(totalTimerTxt != null && playerTimer.IsRunning)
        {
            totalTimerTxt.text = playerTimer.TotalTimer.ToString("F2");
        }
    }

    private void UpdateUIText(float time, bool isNewRecord, float timeDifference)
    {
        Instantiate(initializeTimeBox, CheckpointArea).SetTimeAndDifference(time, timeDifference);
    }

    private void UpdateFinishUIText(float time, bool isNewRecord, float timeDifference)
    {
        yourTotalTimeText.text = time.ToString("F2");
        float recordedTime = saveLoadTimes.LoadData("FinishLine.json");

        if(recordedTime == 0)
        {
            finishTimeText.text = "FINISHING TIME:";
            finishTimeText.enabled = false;
            recordTimeText.enabled = false;
            yourTotalTimeText.enabled = false;
        }
        else if(time <= recordedTime)
        {
            finishTimeText.text = newRecord;
            finishTimeText.enabled = true;
            recordTimeText.enabled = true;
            recordTimeText.text = "New Record Time: " + time.ToString("F2");
            yourTotalTimeText.enabled = false;
        }
        else if(time >= recordedTime)
        {
            finishTimeText.text = yourTime;
            finishTimeText.enabled = true;
            recordTimeText.enabled = true;
            recordTimeText.text = "Record Time is: " + recordedTime.ToString("F2");
            yourTotalTimeText.enabled = true;
            yourTotalTimeText.text = "Your time is: " + time.ToString("F2");
        }
    }
}
