using System.Collections;
using UnityEngine;

public class TrackSwitcher : MonoBehaviour
{
    Cinemachine.CinemachineDollyCart cart;

    public Cinemachine.CinemachineSmoothPath startPath;
    public Cinemachine.CinemachineSmoothPath[] alternatePaths;


    private void Awake()
    {
        cart = GetComponent<Cinemachine.CinemachineDollyCart>();

        ResetCameraToSpectate();
        
    }

    public void ResetCameraToPlayer()
    {
        StopAllCoroutines();

        cart.m_Path = alternatePaths[0];

        StartCoroutine(ChangeTrackToPlayer());
    }
    public void ResetCameraToSpectate()
    {
        StopAllCoroutines();

        cart.m_Path = startPath;

        StartCoroutine(ChangeTrackToSpectate());
    }
    public void ResetCameraToEnemy()
    {
        StopAllCoroutines();

        cart.m_Path = alternatePaths[1];

        StartCoroutine(ChangeTrackToEnemy());
    }

    IEnumerator ChangeTrackToPlayer()
    {
        yield return new WaitForSeconds(Random.Range(4, 6));

        var path = alternatePaths[0];
        cart.m_Path = path;

        StartCoroutine(ChangeTrackToPlayer());
    }
    IEnumerator ChangeTrackToSpectate()
    {
        yield return new WaitForSeconds(Random.Range(4, 6));

        var path = startPath;
        cart.m_Path = path;

        StartCoroutine(ChangeTrackToSpectate());
    }
    IEnumerator ChangeTrackToEnemy()
    {
        yield return new WaitForSeconds(Random.Range(4, 6));

        var path = alternatePaths[1];
        cart.m_Path = path;
    }
}
