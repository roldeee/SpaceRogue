using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsHandler : MonoBehaviour
{
    [SerializeField] CanvasGroup creditsMenu;
    [SerializeField] Canvas startMenu;
    
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && creditsMenu.isActiveAndEnabled)
        {
            creditsMenu.interactable = false;
            creditsMenu.blocksRaycasts = false;
            creditsMenu.alpha = 0f;

            startMenu.enabled = true;
        }
    }
    public void ShowCredits()
    {
        startMenu.enabled = false;

        creditsMenu.interactable = true;
        creditsMenu.blocksRaycasts = true;
        creditsMenu.alpha = 1f;
    }
}
