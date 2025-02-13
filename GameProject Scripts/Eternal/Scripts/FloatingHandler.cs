using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHandler : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 2f;
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
