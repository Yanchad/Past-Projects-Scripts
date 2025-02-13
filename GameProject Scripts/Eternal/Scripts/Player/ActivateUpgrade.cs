using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUpgrade : MonoBehaviour
{
    PlayerShoot playerShoot;
    DashUpgrade dashUpgrade;

    [SerializeField] private GameObject upgrade1;
    [SerializeField] private GameObject upgrade2;
    [SerializeField] private GameObject upgrade3;
    [SerializeField] private GameObject upgrade4;
    [SerializeField] private GameObject upgradeNotification1;
    [SerializeField] private GameObject upgradeNotification2;
    [SerializeField] private GameObject upgradeNotification3;
    [SerializeField] private GameObject upgradeNotification4;

    public bool hasHint1 = false;
    public bool hasHint2 = false;
    public bool hasHint3 = false;
    public bool hasHint4 = false;

    [Header("Audio")]
    [SerializeField] private GameObject collectableSound;

    private void Start()
    {
        playerShoot = GetComponent<PlayerShoot>();
        dashUpgrade = GetComponent<DashUpgrade>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hint1"))
        {
            playerShoot.HasBetterBullets = true;
            Instantiate(upgradeNotification1, new Vector3(collision.transform.position.x + 1, collision.transform.position.y, collision.transform.position.z), Quaternion.identity);
            hasHint1 = true;
            if (collectableSound != null) Instantiate(collectableSound, transform.position, Quaternion.identity); // Sound
        }
        if (collision.CompareTag("Hint2"))
        {
            playerShoot.HasBetterGun = true;
            Instantiate(upgradeNotification2, new Vector3(collision.transform.position.x + 1, collision.transform.position.y, collision.transform.position.z), Quaternion.identity);
            hasHint2 = true;
            if (collectableSound != null) Instantiate(collectableSound, transform.position, Quaternion.identity); // Sound
        }
        if (collision.CompareTag("Hint3"))
        {
            playerShoot.HasShieldUpgrade = true;
            Instantiate(upgradeNotification3, new Vector3(collision.transform.position.x + 1, collision.transform.position.y, collision.transform.position.z), Quaternion.identity);
            hasHint3 = true;
            if (collectableSound != null) Instantiate(collectableSound, transform.position, Quaternion.identity); // Sound
        }
        if (collision.CompareTag("Hint4"))
        {
            Instantiate(upgradeNotification4, new Vector3(collision.transform.position.x + 1, collision.transform.position.y, collision.transform.position.z), Quaternion.identity);
            dashUpgrade.HasDash = true;
            hasHint4 = true;
            if (collectableSound != null) Instantiate(collectableSound, transform.position, Quaternion.identity); // Sound
        }
    }
}
