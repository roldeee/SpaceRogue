using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public Animator animator;
    public GameObject[] waypoints;
    public GameObject player;
    public UnityEngine.AI.NavMeshAgent agent;
    public float chaseRadius = 5f;
    public float attackRadius = 2.5f;
    RoomClearChecker roomClearChecker;
    EventSystem eventSystem;
    
    private bool isDead = false;

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
    public Vector3[] waypointPositions;
    // Start is called before the first frame update
    void Start()
    {
        roomClearChecker = EventSystem.current.GetComponent<RoomClearChecker>();
        if (waypoints.Length > 0)
        {
            waypointPositions = new Vector3[waypoints.Length];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypointPositions[i] = waypoints[i].transform.position;
            }
        }
        animator = GetComponent<Animator>();
        if (waypointPositions.Length > 0)
        {
            aiState = AIState.Roaming;
            setNextWaypoint();
        }
        else
        {
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
        if (waypointPositions.Length != 0)
        {
            currWaypoint = (currWaypoint + 1) % waypointPositions.Length;
            agent.SetDestination(waypointPositions[currWaypoint]);
        }
    }

    private Vector3 getPredictedLocation()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        float lookAheadT = dist / agent.speed;
        Vector3 futureTarget = player.transform.position;// + lookAheadT * player.GetComponent<VelocityReporter>().velocity;
        return futureTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var projectile = collision.collider.GetComponent<Projectile>();
        if (projectile != null)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isDead)
        {
            Destroy(transform.root.gameObject); // delete the parent object.
            roomClearChecker.RemoveEnemy();
            //animator.SetTrigger("isDead"); # TODO: Enable when death animations are setup.
            isDead = true;
        } 
    }
}

