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

    private enum AIState
    {
        Roaming,
        Attacking
    };
    private int epsilon = 1;
    private int currWaypoint = -1;
    private AIState aiState;
    private Vector3[] waypointpositions;
    // Start is called before the first frame update
    void Start()
    {
        aiState = AIState.Roaming;
        setNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        switch (aiState)
        {
            case AIState.Roaming:
                // if the agent is near the player
                float distanceToPlayer = Vector3.Distance(transform.position,
                    player.transform.position);
                if (distanceToPlayer < chaseRadius)
                {
                    aiState = AIState.Attacking;
                }
                else if (!agent.pathPending && agent.remainingDistance < epsilon)
                {
                    setNextWaypoint();
                }
                break;
            case AIState.Attacking:
                agent.SetDestination(getPredictedLocation());
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
