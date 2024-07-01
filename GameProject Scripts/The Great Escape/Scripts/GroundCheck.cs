using UnityEngine;


public class GroundCheck : MonoBehaviour
{
    enum GroundCheckMode { ray, overlapSphere};

    [SerializeField] private GroundCheckMode mode;

    [SerializeField] private bool isGrounded;
    public bool IsGrounded => isGrounded;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float rayLength = 1;

    
    void Update()
    {
        switch (mode)
        {
            case GroundCheckMode.ray:
                isGrounded = Physics.Raycast(transform.position + offset, Vector3.down, rayLength);
                Debug.DrawRay(transform.position + offset, Vector3.down * rayLength);
                break;
            case GroundCheckMode.overlapSphere:
                isGrounded = false;
                Collider[] colliders = Physics.OverlapSphere(transform.position + offset, rayLength);
                foreach (Collider collider in colliders)
                {
                    if(collider.gameObject != this.gameObject)
                    {
                        isGrounded = true;
                    }
                }
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if (mode == GroundCheckMode.overlapSphere) Gizmos.DrawWireSphere(transform.position + offset, rayLength);
    }
}
