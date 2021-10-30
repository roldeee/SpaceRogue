using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    public GameObject player;
    public int numberOfEnemies = 1;
    public int numberOfWaypoints = 2;
    public float spawnRadius = 15f;

    private RoomClearChecker roomClearChecker;

    void Start()
    {
        roomClearChecker = EventSystem.current.GetComponent<RoomClearChecker>();
        roomClearChecker.setNumEnemies(numberOfEnemies);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, GetRandomLocationOnNavMesh(spawnRadius), Quaternion.identity);
            EnemyAI ai = newEnemy.GetComponentInChildren<EnemyAI>();
            ai.player = player;
            Vector3[] randomWaypoints = new Vector3[numberOfWaypoints];
            for (int j = 0; j < numberOfWaypoints; j++)
            {
                Vector3 location = GetRandomLocationOnNavMesh(spawnRadius);
                randomWaypoints[j] = location;
            }
            ai.waypointPositions = randomWaypoints;
        }
    }

    private Vector3 GetRandomLocationOnNavMesh(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;            
        }
        return finalPosition;        
    }
}
