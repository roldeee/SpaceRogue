using UnityEngine;
using System.Collections;

public class PersistedDataHelper
{
    public static int GetWinStreak()
    {
        if (!PlayerPrefs.HasKey("WinStreak"))
        {
            PlayerPrefs.SetInt("WinStreak", 0);
        }
        return PlayerPrefs.GetInt("WinStreak");
    }

    public static void ResetWinStreak()
    {
        PlayerPrefs.SetInt("WinStreak", 0);
    }

    public static void IncrementWinStreak()
    {
        int currentWinStreak = GetWinStreak();
        PlayerPrefs.SetInt("WinStreak", currentWinStreak + 1);
    }
}
