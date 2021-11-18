using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossManager : MonoBehaviour
{
    private RoomClearChecker roomClearChecker;

    // Start is called before the first frame update
    void Start()
    {
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
