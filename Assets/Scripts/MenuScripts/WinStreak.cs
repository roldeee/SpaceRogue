using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinStreak : MonoBehaviour
{
    private Text winStreakText;

    // Use this for initialization
    void Start()
    {
        winStreakText = GetComponent<Text>();
        SetWinStreakText();
    }

    public void SetWinStreakText()
    {
        winStreakText.text = "Win Streak: " + PersistedDataHelper.GetWinStreak();
    }
}
