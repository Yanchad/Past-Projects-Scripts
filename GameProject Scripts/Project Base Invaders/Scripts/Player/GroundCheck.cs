using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    enum GroundCheckMode { ray, overlapSphere};

    [SerializeField] private GroundCheckMode mode;

    [SerializeField] private bool isGrounded;
    public bool IsGrounded => isGrounded;

    [SerializeField] private bool isOnGreenhouse;
    public bool IsOnGreenhouse => isOnGreenhouse;

    [SerializeField] private bool isOnResearchLab;
    public bool IsOnResearchLab => isOnResearchLab;

    [SerializeField] private bool isOnH3Mine;
    public bool IsOnH3Mine => isOnH3Mine;

    [SerializeField] private bool isOnMechanic;
    public bool IsOnMechanic => isOnMechanic;
    [SerializeField] private bool isOnTerrain;
    public bool IsOnTerrain => isOnTerrain;
    private bool isOnLZ;
    public bool IsOnLZ => isOnLZ;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float rayLength = 1;
    

    void Update()
    {
        switch (mode)
        {
            case GroundCheckMode.ray:
                isGrounded = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength);
                Debug.DrawRay(transform.position + offset, Vector2.down * rayLength);
                break;
            case GroundCheckMode.overlapSphere:
                isGrounded = false;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + offset, rayLength);
                foreach(Collider2D collider in colliders)
                {
                    if(collider.gameObject != this.gameObject && collider.gameObject.tag != "Building" && collider.gameObject.tag != "Enemy" && collider.gameObject.tag != "PatrolPoint" &&
                       collider.gameObject.tag != "Planet" && collider.gameObject.tag != "OutOfBounds" && collider.gameObject.tag != "OOBject" && collider.gameObject.tag != "EnemyBullet" 
                       && collider.gameObject.tag != "InsideShip")
                    {
                        isGrounded = true;
                    }
                    
                    if (collider.gameObject.tag == "Greenhouse")
                    {
                        isOnGreenhouse = true;
                        isOnLZ = true;
                    }
                    else
                    {
                        isOnGreenhouse = false;
                        isOnLZ = false;
                    }
                    if (collider.gameObject.tag == "ResearchLab")
                    {
                        isOnResearchLab = true;
                        isOnLZ = true;
                    }
                    else
                    {
                        isOnResearchLab = false;
                        isOnLZ = false;
                    }
                    if (collider.gameObject.tag == "H3Mine")
                    {
                        isOnH3Mine = true;
                        isOnLZ = true;
                    }
                    else
                    {
                        isOnH3Mine = false;
                        isOnLZ = false;
                    }
                    if (collider.gameObject.tag == "Mechanic")
                    {
                        isOnMechanic = true;
                        isOnLZ = true;
                    }
                    else
                    {
                        isOnMechanic = false;
                        isOnLZ = false;
                    }
                    if (collider.gameObject.tag == "Terrain")
                    {
                        isOnTerrain = true;
                    }
                    else isOnTerrain = false;
                }
                break;
        }
    }
    private void OnDrawGizmos()
    {
        if(mode == GroundCheckMode.overlapSphere) Gizmos.DrawWireSphere(transform.position + offset, rayLength);
    }
}
