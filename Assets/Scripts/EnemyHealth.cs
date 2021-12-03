using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyHealth : MonoBehaviour
{
    public EnemyHealthBar enemyHealthBar;

    public int health
    {
        get;
        private set;
    } = 100;

    private void Update()
    {
/*        if (health <= 0)
        {
            Destroy(gameObject);
        }*/
    }

    public void SetHealth(int health)
    {
        this.health = health;
        enemyHealthBar.SetMaxHealth(health);
    }

    public void TakeDamage(int damage)
    {
        EventManager.TriggerEvent<EnemyDamagedEvent, Vector3>(transform.position);
        health -= damage;
        enemyHealthBar.SetHealth(health);
    }
}
