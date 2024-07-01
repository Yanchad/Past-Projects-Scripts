using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipShoot : MonoBehaviour
{
    GroundCheck playerGroundCheck;
    BossMove bossMove;
    InsidePlayerCheck insidePlayerCheck;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos1;
    [SerializeField] private Transform bulletPos2;
    [SerializeField] private Transform bulletPos3;
    [SerializeField] private Transform bulletPos4;
    [SerializeField] private Transform bulletPos5;
    [SerializeField] private Transform bulletPos6;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioSource audioSource3;

    private float timer;
    [SerializeField] private float shootInterval = 2;

    void Start()
    {
        insidePlayerCheck = GetComponentInChildren<InsidePlayerCheck>();
        playerGroundCheck = FindObjectOfType<GroundCheck>();
        bossMove = GetComponent<BossMove>();
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval && playerGroundCheck.IsGrounded == false && bossMove.HasReachedTarget)
        {
            timer = 0;
            Shoot();
        }
    }
    private void Shoot()
    {
        if(insidePlayerCheck.IsInside == false)
        {
            audioSource.Play();
            audioSource2.Play();
            audioSource3.Play();
            Instantiate(bullet, bulletPos1.position, Quaternion.identity);
            Instantiate(bullet, bulletPos2.position, Quaternion.identity);
            Instantiate(bullet, bulletPos3.position, Quaternion.identity);
            Instantiate(bullet, bulletPos4.position, Quaternion.identity);
            Instantiate(bullet, bulletPos5.position, Quaternion.identity);
            Instantiate(bullet, bulletPos6.position, Quaternion.identity);
        }
    }
}
