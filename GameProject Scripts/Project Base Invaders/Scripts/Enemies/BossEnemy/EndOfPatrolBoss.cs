using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfPatrolBoss : MonoBehaviour
{
    PatrolTheSkyBoss patrolTheSkyBoss;
    private float moveSpeedModifier = 0.4f;

    void Start()
    {
        patrolTheSkyBoss = GetComponent<PatrolTheSkyBoss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PatrolPoint")
        {
            patrolTheSkyBoss.MoveSpeed *= moveSpeedModifier;
        }
    }
}
