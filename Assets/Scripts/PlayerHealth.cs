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

        // Enable all health ticks
        for (int i = 0; i < healthTicks.Length; i++)
        {
            if (i < currentHealthTicks)
            {
                healthTicks[i].enabled = true;
            }
            else
            {
                healthTicks[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        // Disable health ticks
        int newHealthTicks = currentHealthTicks - damage;
        for (int i = currentHealthTicks; i > newHealthTicks - 1; i--)
        {
            healthTicks[i].enabled = false;
        }

        // Set new health
        currentHealthTicks = newHealthTicks;
        Debug.Log("Health: " + currentHealthTicks);

        // Show Game Over Menu if dead
        if (currentHealthTicks <= 0)
        {
            GameObject gomObject = GameObject.Find("GameOverMenu");
            GameOverMenu gameOverMenu = gomObject.GetComponent<GameOverMenu>();
            gameOverMenu.ShowGameOver();
        }
    }

    public int GetPlayerHealth()
    {
        return currentHealthTicks;
    }
}
