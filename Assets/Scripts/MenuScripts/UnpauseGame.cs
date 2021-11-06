using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpauseGame : MonoBehaviour
{
    public Canvas canvas;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("No canvas group attached to the pause menu!");
        }
    }
    public void unpause()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
