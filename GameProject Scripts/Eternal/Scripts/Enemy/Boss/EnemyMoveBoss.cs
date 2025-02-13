using UnityEngine;

public class EnemyMoveBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Transform rangeOrigin;
    [SerializeField] private Transform target;

    [SerializeField] private float aggroRange;
    [SerializeField] private float stopRange;

    private float distance;
    private bool isInRange;
    private bool isTooClose;
    [SerializeField] private bool reverseDirection;

    private Vector2 directionToPlayer;
    private Vector2 targetDirection;
    private Vector2 obstacleAvoidanceTargetDirection;
    private float changeDirectionCooldown;
    private float obstacleAvoidanceCooldown;
    private float reverseTimer;

    [Header("Obstacle Avoidance")]
    [SerializeField] private float reverseDuration = 1.5f;
    [SerializeField] private float obstacleCheckCircleRadius;
    [SerializeField] private float obstacleCheckDistance;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private LayerMask bostacleLayerMask2;

    private RaycastHit2D[] obstacleCollisions;

    public bool IsInRange => isInRange;
    public Vector2 TargetDirection => targetDirection;
    public float AggroRange => aggroRange;

    private void Awake()
    {
        targetDirection = transform.up;
        obstacleCollisions = new RaycastHit2D[10];
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }
    private void Update()
    {
        if(reverseTimer > 0)
        {
            reverseTimer -= Time.deltaTime;
        }
        else reverseDirection = false;
    }

    private void UpdateTargetDirection()
    {
        HandleAggroDirection();
        //HandlePlayerTargeting();
        HandleRandomDirectionChange();
        HandleObstacles();
    }
    private void HandleRandomDirectionChange()
    {
        changeDirectionCooldown -= Time.deltaTime;

        if(changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            targetDirection = rotation * targetDirection;

            changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }
    private void HandleAggroDirection()
    {
        Vector2 enemyToPlayerVector = target.position - transform.position;
        directionToPlayer = enemyToPlayerVector.normalized;

        if(enemyToPlayerVector.magnitude <= aggroRange)
        {
            isInRange = true;
        }
        else
        {
             isInRange = false;
        }
        if (enemyToPlayerVector.magnitude <= stopRange)
        {
            isTooClose = true;
        }
        else isTooClose = false;
    }
    private void HandlePlayerTargeting()
    {
        if (isInRange && HasLineOfSight())
        {
            targetDirection = directionToPlayer;
        }
    }
    private void HandleObstacles()
    {
        obstacleAvoidanceCooldown -= Time.fixedDeltaTime;

        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(obstacleLayerMask);

        int numberOfCollisions = Physics2D.CircleCast(transform.position, obstacleCheckCircleRadius, transform.up, contactFilter, obstacleCollisions, obstacleCheckDistance);
        for (int i = 0; i < numberOfCollisions; i++)
        {
            var obstacleCollision = obstacleCollisions[i];

            if(obstacleCollision.collider.gameObject == gameObject)
            {
                continue;
            }
            if (obstacleAvoidanceCooldown <= 0)
            {
                obstacleAvoidanceTargetDirection = obstacleCollision.normal;
                obstacleAvoidanceCooldown = 0.5f;
            }
            Quaternion rotZ = Quaternion.Euler(0, 0, rb.rotation);
            var targetRotation = Quaternion.LookRotation(transform.forward, obstacleAvoidanceTargetDirection);
            var rotation = Quaternion.RotateTowards(rotZ, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            targetDirection = rotation * Vector2.up;
            break;
        }
    }
    private void RotateTowardsTarget()
    {
        Quaternion rotZ = Quaternion.Euler(0, 0, rb.rotation);
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(rotZ, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        rb.MoveRotation(rotation);
    }
    private void SetVelocity()
    {
        if (isTooClose) rb.velocity = Vector3.zero;
        else if (reverseDirection && reverseTimer > 0) rb.velocity = -(transform.up * speed * 0.3f * Time.fixedDeltaTime);
        else rb.velocity = transform.up * speed * Time.fixedDeltaTime;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(rangeOrigin.position , aggroRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rangeOrigin.position , stopRange);
    }
    private bool HasLineOfSight()
    {
        // Cast a ray from gun to the player to check for obstacles
        Vector2 direction = target.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, distance, bostacleLayerMask2);

        // Return true if the raycast doesn't hit an obstacle
        return hit.collider == null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            reverseDirection = true;
            reverseTimer = reverseDuration;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            reverseDirection = false;
        }
    }
}
