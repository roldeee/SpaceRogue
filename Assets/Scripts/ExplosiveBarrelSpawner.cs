using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosiveBarrelSpawner : MonoBehaviour
{
    [SerializeField] int numBarrels;
    private static string BARREL_PATH = "Barrel/FlamableBarrel";

    void Awake()
    {
        if (shouldSpawnBarrel())
        {
            spawnBarrel();
        }
    }

    private bool shouldSpawnBarrel()
    {
        return true;
    }

    private void spawnBarrel()
    {
        for (int i = 0; i < numBarrels; i++)
        {
            GameObject barrel = Resources.Load<GameObject>(BARREL_PATH);
            Instantiate(barrel, GetPosition(), Quaternion.identity);
        }
    }

    Vector3 GetPosition()
    {
        NavMeshTriangulation navMesh = NavMesh.CalculateTriangulation();

        int i = Random.Range(0, navMesh.indices.Length - 3);

        Vector3 a = navMesh.vertices[navMesh.indices[i]];
        Vector3 b = navMesh.vertices[navMesh.indices[i + 1]];
        Vector3 c = navMesh.vertices[navMesh.indices[i + 2]];

        Vector3 position = Vector3.Lerp(a, b, Random.value);
        Vector3.Lerp(position, c, Random.value);

        return position;
    }
}
