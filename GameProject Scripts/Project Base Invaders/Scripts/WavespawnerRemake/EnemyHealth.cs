using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Event triggered when the enemy is destroyed
    public event Action OnDestroyed;

    // Other properties and methods related to enemy health...

    // Call this method when the enemy is destroyed
    private void DestroyEnemy()
    {
        // Perform any necessary cleanup or logic when the enemy is destroyed...

        // Trigger the OnDestroyed event
        if (OnDestroyed != null)
        {
            OnDestroyed.Invoke();
        }

        // Optionally, you may want to disable the GameObject or destroy it here
        //gameObject.SetActive(false);
        // or
        Destroy(gameObject);
    }
}
