using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> enemyPrefabs;
    public GameObject player;
    public int numberOfEnemies = 1;
    public int enemyScale = 1;
    public int numberOfWaypoints = 2;
    public float spawnRadius = 15f;

    private RoomClearChecker roomClearChecker;

    void Start()
    {
        roomClearChecker = EventSystem.current.GetComponent<RoomClearChecker>();
        // get player data
        PlayerData playerData = PlayerDataManager.Instance.playerData;
        // get num rooms cleared
        int numRoomsCleared = playerData.numRoomsCleared;
        // get win streak
        int winStreak = PersistedDataHelper.GetWinStreak();
        // write formula for number of enemies
        int enemyCalc = ((winStreak * 2 + 1) * enemyScale) + (int)(numRoomsCleared * 2 * 1.5f);
        //Debug.Log("连胜：" + winStreak + "/当前闯关房间：" + numRoomsCleared + "/当前怪物血量：" + enemyCalc);
        numberOfEnemies = Mathf.Min(enemyCalc, enemyScale * 8);
        roomClearChecker.setNumEnemies(numberOfEnemies);
        Debug.Log("Num enemies: " + numberOfEnemies.ToString());
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], GetRandomLocationOnNavMesh(spawnRadius), Quaternion.identity);
            EnemyAI ai = newEnemy.GetComponentInChildren<EnemyAI>();
            ai.player = player;

            // Set Enemy Health based on difficulty (each win increases number of hits it takes to kill an enemy)
            EnemyHealth enemyHealth = newEnemy.GetComponentInChildren<EnemyHealth>();
            enemyHealth.SetHealth(10 + (5 * (PersistedDataHelper.GetWinStreak())));

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
