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
    public float speed = 3f;
    RoomClearChecker roomClearChecker;
    EventSystem eventSystem;
    EnemyHealth enemyHealth;

    private Collider[] ragdollColliders;
    private Rigidbody rb;
    private bool isDead = false;

    private enum AIState
    {
        Roaming,
        Attacking,
        Chasing,
        Idle,
        Dead
    };
    private int epsilon = 1;
    private int currWaypoint = -1;
    private AIState aiState;
    public Vector3[] waypointPositions;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        roomClearChecker = EventSystem.current.GetComponent<RoomClearChecker>();
        rb = GetComponent<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        EnableRagdoll(false);
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
        if (isDead)
            aiState = AIState.Dead;
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
                    transform.LookAt(player.transform, Vector3.up);
                    animator.SetBool("Attacking", true);
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
            case AIState.Dead:
                agent.enabled = false;
                animator.SetBool("Attacking", false);
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
        Projectile projectile = collision.collider.GetComponent<Projectile>();
        if (projectile != null)
        {
            TakeDamage(projectile.damage);
        }
    }

    /*    private void OnTriggerEnter(Collider other)
        {
            if (isDead)
                return;
            if (other.tag.Equals("Projectile"))
            {
                TakeDamage(other.GetComponent<Projectile>().damage);
            }
        }*/

    public void TakeDamage(int damage)
    {
        enemyHealth.TakeDamage(damage);
        if (enemyHealth.health <= 0 && !isDead)
        {
            Destroy(transform.root.gameObject);
            roomClearChecker.RemoveEnemy();
            isDead = true;
        }
    }

    public void TakeExplosiveDamage(int damage, Vector3 explosionPosition)
    {
        if (isDead)
            return;
        enemyHealth.TakeDamage(damage);
        EnableRagdoll(true);
        gameObject.layer = 9; // layer 9 = dead enemies
        foreach (Collider c in ragdollColliders)
        {
            if (c.GetComponent<Rigidbody>() != null) 
            {
                ragdollColliders[0].enabled = false;
                ragdollColliders[1].enabled = false;
                c.gameObject.layer = 9;
                c.GetComponent<Rigidbody>().AddExplosionForce(1f, explosionPosition, 3f, 3f, ForceMode.Impulse);
            }
        }

        if (enemyHealth.health <= 0 && !isDead)
        {
            Destroy(transform.root.gameObject, 5);
            roomClearChecker.RemoveEnemy();
            isDead = true;
        }
    }

    private void EnableRagdoll(bool status)
    {
        for (int i = 2; i < ragdollColliders.Length; i++)
        {
            ragdollColliders[i].enabled = status;
        }
        animator.enabled = !status;
    }
}

