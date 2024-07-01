using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;

    private float timer;
    [SerializeField] private float shootInterval = 2;
    [SerializeField] private AudioSource audioSource;



    void Update()
    {

        timer += Time.deltaTime;

        if (timer > shootInterval)
        {
            timer = 0;
            Shoot();
        }
    }


    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        audioSource.Play();
    }

}
