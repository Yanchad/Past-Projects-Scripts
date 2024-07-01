using UnityEngine;

public class DiceSide : MonoBehaviour
{
    bool onGround;
    public int sideValue;

    private void OnTriggerStay(Collider col)
    {
        if(col.tag == "Ground")
        {
            onGround = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Ground")
        {
            onGround = false;
        }
    }

    public bool OnGround()
    {
        return onGround;
    }

}
