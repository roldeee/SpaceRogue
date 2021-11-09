using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CanvasGroup))]

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverText;
    public bool menuVisible
    {
        get;
        private set;
    } = false;

    private CanvasGroup canvasGroup;
    private TMPro.TextMeshProUGUI gameOverTextMesh;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("No canvas group attached to the pause menu!");
        }

        gameOverTextMesh = gameOverText.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void ShowGameOver(bool finished = false)
    {
        // Set the text when game is finished
        if (finished)
        {
            gameOverTextMesh.text = "You Won!";
        } else
        {
            gameOverTextMesh.text = "Game Over!";
        }

        menuVisible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }

    public void HideGameOver()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        menuVisible = false;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
    }
}
