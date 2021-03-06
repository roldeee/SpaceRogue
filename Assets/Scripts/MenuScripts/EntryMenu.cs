using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]

public class EntryMenu : MonoBehaviour
{
    public bool menuVisible
    {
        get;
        private set;
    } = false;

    private CanvasGroup canvasGroup;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("No canvas group attached to the entry menu!");
        }
        ShowEntryMenu();
    }

    void ShowEntryMenu()
    {
        menuVisible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }

    public void CloseEntryMenu()
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
