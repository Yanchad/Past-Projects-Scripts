using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float playerScore;
    [SerializeField] private float currency;

    public float Currency { get {  return currency; } set { currency = value; } }
    public float PlayerScore { get { return playerScore; } set { playerScore = value; } }
}

