using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth = 100;

    private void Update()
    {
        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
