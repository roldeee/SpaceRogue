using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public Animator animator;
    public GameObject[] waypoints;
    public GameObject player;
    public UnityEngine.AI.NavMeshAgent agent;
    public float chaseRadius = 5f;
    public float attackRadius = 2.5f;

    private enum AIState
    {
        Roaming,
        Attacking,
        Chasing,
        Idle
    };
    private int epsilon = 1;
    private int currWaypoint = -1;
    private AIState aiState;
    private Vector3[] waypointpositions;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (waypoints.Length > 0) {
            aiState = AIState.Roaming;
            setNextWaypoint();
        } else {
            aiState = AIState.Idle;
            agent.isStopped = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position,
            player.transform.position);
        animator.SetBool("Attacking", false);
        animator.SetFloat("Speed", agent.velocity.magnitude);
        switch (aiState)
        {
            case AIState.Roaming:
                // if the agent is near the player
                if (distanceToPlayer < chaseRadius)
                {
                    aiState = AIState.Chasing;
                }
                else if (!agent.pathPending && agent.remainingDistance < epsilon)
                {
                    setNextWaypoint();
                }
            break;
            case AIState.Chasing:
                if (distanceToPlayer < attackRadius) 
                {
                    aiState = AIState.Attacking;
                    agent.isStopped = true;
                }
                else 
                {
                    agent.SetDestination(getPredictedLocation());
                }
            break;
            case AIState.Attacking:
                if (distanceToPlayer >= attackRadius) 
                {
                    aiState = AIState.Chasing;
                    agent.isStopped = false;
                }
                else 
                {
                    animator.SetBool("Attacking", true);
                    // animate
                }
            break;
            case AIState.Idle:
                if (distanceToPlayer < chaseRadius)
                {
                    aiState = AIState.Chasing;
                }
                else if (distanceToPlayer < attackRadius) 
                {
                    aiState = AIState.Attacking;
                    agent.isStopped = true;
                }
            break;
        }
    }

    private void setNextWaypoint()
    {
        if (waypoints.Length != 0)
        {
            currWaypoint = (currWaypoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currWaypoint].transform.position);
        }
    }

    private Vector3 getPredictedLocation()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        float lookAheadT = dist / agent.speed;
        Vector3 futureTarget = player.transform.position;// + lookAheadT * player.GetComponent<VelocityReporter>().velocity;
        return futureTarget;
    }
}
