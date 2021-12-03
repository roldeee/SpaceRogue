using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosiveBarrelSpawner : MonoBehaviour
{
    //[SerializeField] GameObject[] waypoints;
    [SerializeField] int numBarrels;
    private static string BARREL_PATH = "Barrel/FlamableBarrel";

    void Awake()
    {
        if (shouldSpawnBarrel()/* && waypoints != null && waypoints.Length > 0*/)
        {
            spawnBarrel();
        }
    }

    private bool shouldSpawnBarrel()
    {
        return true/*Random.value <= 0.3f*/;
    }

    private void spawnBarrel()
    {
/*        if (numBarrels > waypoints.Length)
        {
            Debug.Log("Number of desired barrels is greater than the number of waypoints.");
            return;
        }*/
/*        bool[] seen = new bool[waypoints.Length];
        for (int i = 0; i < numBarrels; i++)
        {
            int randomWaypoint = Random.Range(0, waypoints.Length);
            while (seen[randomWaypoint])
            {
                randomWaypoint = Random.Range(0, waypoints.Length);
            }
            seen[randomWaypoint] = true;
            GameObject barrel = Resources.Load<GameObject>(BARREL_PATH);
            Instantiate(barrel, waypoints[randomWaypoint].transform.position, Quaternion.identity);
        }*/
        
        for (int i = 0; i < numBarrels; i++)
        {
            GameObject barrel = Resources.Load<GameObject>(BARREL_PATH);
            Instantiate(barrel, GetRandomLocation(), Quaternion.identity);
        }
    }

    Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick the first indice of a random triangle in the nav mesh
        int t = Random.Range(0, navMeshData.indices.Length - 3);

        // Select a random point on it
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        return point;
    }
}
