using UnityEngine;

public class ItemController : MonoBehaviour, IDrag
{

    //private Rigidbody rb;
    private Rigidbody2D rb2D;


    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        rb2D = GetComponent<Rigidbody2D>();
    }


    public void onEndDrag()
    {
        //rb.useGravity = true;
        rb2D.gravityScale = 1f;
    }
    public void onStartDrag()
    {
        //rb.useGravity = false;
        rb2D.gravityScale = 0f;
        rb2D.velocity = Vector3.zero;
    }

}
