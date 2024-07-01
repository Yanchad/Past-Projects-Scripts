using UnityEngine;

public class DiceSideEnemy : MonoBehaviour
{
    bool onGroundEnemy;
    public int sideValueEnemy;
    
    private void OnTriggerStay(Collider col)
    {
        if(col.tag == "Ground")
        {
            onGroundEnemy = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Ground")
        {
            onGroundEnemy = false;
        }
    }

    public bool OnGroundEnemy()
    {
        return onGroundEnemy;
    }

}
