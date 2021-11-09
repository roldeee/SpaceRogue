using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SpawnScene");
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        PlayerDataManager playerDataManager = PlayerDataManager.Instance;
        playerDataManager.playerData = new PlayerData();
    }
}
