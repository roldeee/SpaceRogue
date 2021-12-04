using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossManager : MonoBehaviour
{
    private RoomClearChecker roomClearChecker;
    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Set Enemy Health based on difficulty (each win increases number of hits it takes to kill an enemy)
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.SetHealth(150  + (100 * (PersistedDataHelper.GetWinStreak())));

        roomClearChecker = EventSystem.current.GetComponent<RoomClearChecker>();
        roomClearChecker.setNumEnemies(1);
    }

    private void OnDestroy()
    {
        // Save win streak
        PersistedDataHelper.IncrementWinStreak();

        GameObject gomObject = GameObject.Find("GameOverMenu");
        if (gomObject != null)
        {
            GameOverMenu gameOverMenu = gomObject.GetComponent<GameOverMenu>();
            gameOverMenu.ShowGameOver(true);
        }
    }
}
