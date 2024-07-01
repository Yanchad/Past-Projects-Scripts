using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMove : MonoBehaviour
{

    [SerializeField] private GameObject targetDestination;
    
    [SerializeField] private float bossMoveSpeed;
    
    private bool hasReachedTarget;

    public bool HasReachedTarget => hasReachedTarget;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetDestination.transform.position, bossMoveSpeed * Time.deltaTime);
        if(transform.position.y == targetDestination.transform.position.y) hasReachedTarget = true;
    }
}
