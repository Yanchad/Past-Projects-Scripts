using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove2_0 : MonoBehaviour
{
    [SerializeField] private WallCheck wallCheck;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject gameObjectToRotate;
    [SerializeField] private float updateDirectionTime = 1f;
    [SerializeField] private float maxDistance = 2f;
    [SerializeField] private Transform target;

    NavMeshAgent agent;

    private float timer;

    private void Awake()
    {
        
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        TargetPosUpdate();
        MoveTowardsTarget();
        RotateTowardsTarget();
    }

    private void TargetPosUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= updateDirectionTime || wallCheck.WallCollide)
        {
            // Generate a random offset from the current position within a set distance
            Vector2 randomDirection = Random.insideUnitCircle * maxDistance;

            // Set the new target position by adding the random direction to the enemy's current position
            Vector3 newTargetPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0);

            // Set the target position
            target.position = newTargetPosition;

            // Reset the timer for the next update
            timer = 0;
            wallCheck.WallCollide = false;
        }
    }

    private void MoveTowardsTarget()
    {
        // Ensure we have a valid target position
        if (target != null)
        {
            // Make the enemy move towards the target using the NavMeshAgent
            agent.SetDestination(target.position);
        }
    }

    private void RotateTowardsTarget()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.z = 0f; // Keep the rotation on the 2D plane

            // Create the desired rotation towards the target
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);

            // Rotate smoothly towards the target (with a max rotation speed)
            gameObjectToRotate.transform.rotation = Quaternion.RotateTowards(gameObjectToRotate.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
