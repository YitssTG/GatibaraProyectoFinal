using UnityEngine;
using UnityEngine.AI;

public class ReactiveMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public float detectionRange = 5f;

    private NavMeshAgent agent;
    private int patrolIndex = 0;
    private bool chasingPlayer = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(patrolPoints[patrolIndex].position);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            chasingPlayer = true;
            agent.SetDestination(player.position);
        }
        else if (chasingPlayer)
        {
            chasingPlayer = false;
            agent.SetDestination(patrolPoints[patrolIndex].position);
        }

        if (!chasingPlayer && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[patrolIndex].position);
        }
    }
}
