
using UnityEngine;

public class OffscreenEngineToggle : MonoBehaviour
{

    private Vector3 position;
    private bool isOutOfBounds;
    public bool IsOutOfBounds => isOutOfBounds;


    void Update()
    {
        position = transform.position;
        if (position.y > 9f)
        {
            isOutOfBounds = true;
        }
        else isOutOfBounds = false;
    }
}
