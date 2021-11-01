using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int currentHealthTicks;
    private PlayerDataManager playerDataManager;
    public Image[] healthTicks;
    public Sprite healthTick;

    private void Start()
    {
        playerDataManager = PlayerDataManager.Instance;
        currentHealthTicks = playerDataManager.playerData.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < healthTicks.Length; i++)
        {
            if (i < currentHealthTicks)
            {
                healthTicks[i].enabled = true;
            } else
            {
                healthTicks[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealthTicks -= damage;
        Debug.Log("Health: " + currentHealthTicks);
    }

    public int GetPlayerHealth()
    {
        return currentHealthTicks;
    }
}
