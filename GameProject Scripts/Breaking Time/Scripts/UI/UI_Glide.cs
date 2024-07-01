using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Glide : MonoBehaviour, IOnGlideTimeChanged
{
    [Header("Assignables")]
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Image glideBarIMG;

    private void OnEnable()
    {
        playerMove.RegisterListener(this);
    }
    private void OnDisable()
    {
        playerMove.RemoveListener(this);
    }

    public void OnGlideTimeChanged(float time)
    {
        

        glideBarIMG.fillAmount = time;
    }
}
