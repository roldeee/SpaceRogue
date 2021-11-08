using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]

public class PauseMenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private GameOverMenu gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("No canvas group attached to the pause menu!");
        }

        GameObject gomObject = GameObject.Find("GameOverMenu");
        if (gomObject == null) {
            Debug.LogError("Cannot find GameOverMenu game object!");
        }
        gameOverMenu = gomObject.GetComponent<GameOverMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            // Close Menu
            if (canvasGroup.interactable)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Time.timeScale = 1f;
            }
            // Open Menu (if the game over menu is not visible)
            else if (!gameOverMenu.menuVisible)
            {
                Cursor.visible = true;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
            }
        }
    }
}
