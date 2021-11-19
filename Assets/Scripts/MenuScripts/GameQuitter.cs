using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameQuitter : MonoBehaviour
{
    public bool quitOnX = false;

    void Update()
    {
        if (quitOnX && Input.GetKeyUp(KeyCode.X))
            QuitGame();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
