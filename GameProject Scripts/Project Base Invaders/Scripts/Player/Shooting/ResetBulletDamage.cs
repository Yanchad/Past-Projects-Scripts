using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ResetBulletDamage : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private int defaultDamageValue;
    
    Bullet bullet;


    
    void Start()
    {
        bullet = bulletPrefab.GetComponent<Bullet>();

        bullet.HPDAMAGE = defaultDamageValue;
    }
}
