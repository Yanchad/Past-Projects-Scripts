using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BarrelRotation : MonoBehaviour
{
    PlayerHealth playerHealth;

    [SerializeField] private Transform turretRotationPoint1;
    [SerializeField] private Transform turretRotationPoint2;
    [SerializeField] private Transform turretRotationPoint3;
    [SerializeField] private Transform turretRotationPoint4;
    [SerializeField] private Transform turretRotationPoint5;
    [SerializeField] private Transform turretRotationPoint6;


    [SerializeField] private float rotateSpeed = 10;

    private Transform targetToRotateTowards;

    private void Awake()
    {
        targetToRotateTowards = GameObject.Find("Player").GetComponent<Transform>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if(playerHealth.Health > 0) RotateTowardsTarget();
    }



    private void RotateTowardsTarget()
    {
        Vector3 direction1 = targetToRotateTowards.transform.position - turretRotationPoint1.position;
        float angle1 = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg + 90;

        Vector3 direction2 = targetToRotateTowards.transform.position - turretRotationPoint2.position;
        float angle2 = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg + 90;

        Vector3 direction3 = targetToRotateTowards.transform.position - turretRotationPoint3.position;
        float angle3 = Mathf.Atan2(direction3.y, direction3.x) * Mathf.Rad2Deg + 90;

        Vector3 direction4 = targetToRotateTowards.transform.position - turretRotationPoint4.position;
        float angle4 = Mathf.Atan2(direction4.y, direction4.x) * Mathf.Rad2Deg + 90;

        Vector3 direction5 = targetToRotateTowards.transform.position - turretRotationPoint5.position;
        float angle5 = Mathf.Atan2(direction5.y, direction5.x) * Mathf.Rad2Deg + 90;

        Vector3 direction6 = targetToRotateTowards.transform.position - turretRotationPoint6.position;
        float angle6 = Mathf.Atan2(direction6.y, direction6.x) * Mathf.Rad2Deg + 90;

        Quaternion rotation1 = Quaternion.AngleAxis(angle1, Vector3.forward);
        Quaternion rotation2 = Quaternion.AngleAxis(angle2, Vector3.forward);
        Quaternion rotation3 = Quaternion.AngleAxis(angle3, Vector3.forward);
        Quaternion rotation4 = Quaternion.AngleAxis(angle4, Vector3.forward);
        Quaternion rotation5 = Quaternion.AngleAxis(angle5, Vector3.forward);
        Quaternion rotation6 = Quaternion.AngleAxis(angle6, Vector3.forward);
        turretRotationPoint1.transform.rotation = Quaternion.Slerp(turretRotationPoint1.rotation, rotation1, rotateSpeed * Time.deltaTime);
        turretRotationPoint2.transform.rotation = Quaternion.Slerp(turretRotationPoint2.rotation, rotation2, rotateSpeed * Time.deltaTime);
        turretRotationPoint3.transform.rotation = Quaternion.Slerp(turretRotationPoint3.rotation, rotation3, rotateSpeed * Time.deltaTime);
        turretRotationPoint4.transform.rotation = Quaternion.Slerp(turretRotationPoint4.rotation, rotation4, rotateSpeed * Time.deltaTime);
        turretRotationPoint5.transform.rotation = Quaternion.Slerp(turretRotationPoint5.rotation, rotation5, rotateSpeed * Time.deltaTime);
        turretRotationPoint6.transform.rotation = Quaternion.Slerp(turretRotationPoint6.rotation, rotation6, rotateSpeed * Time.deltaTime);
    }
}
