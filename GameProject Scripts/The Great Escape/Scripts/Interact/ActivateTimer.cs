using UnityEngine;

public class ActivateTimer : MonoBehaviour, IInteractable
{

    private bool timerActive;

    public bool TimerActive { get { return timerActive; } private set {; } }


    private void Start()
    {
        timerActive = false;
    }


    public void Interact()
    {
        timerActive = true;
        SpeedrunTimer.Instance.watch.Start();
        SpeedrunTimer.Instance.timerText.enabled = true;
    }

    public string Look()
    {
        string textToReturn = "Set a timer";
        return textToReturn;
    }
}
