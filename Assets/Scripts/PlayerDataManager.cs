using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager
{
    public PlayerData playerData;
    public PlayerDataManager()
    {
        playerData = new PlayerData();
        Save();
    }

    public void Save()
    {
        Debug.Log("Saving data...");
        LogData();
        PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(playerData));
        PlayerPrefs.Save();
        Debug.Log("Saved successful.");
    }

    public void Load()
    {
        Debug.Log("Loading data...");
        playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("PlayerData"));
        LogData();
        Debug.Log("Load successful.");
    }

    public void LogData()
    {
        Debug.Log("Score: " + playerData.score);
        Debug.Log("Current Health: " + playerData.currentHealth);
        Debug.Log("Max Health: " + playerData.maxHealth);
        if (playerData.nextReward.HasValue)
            Debug.Log("Next Reward: " + playerData.nextReward.Value);
        Debug.Log("Current Rewards: " + string.Join(", " , playerData.currentRewards));
    }
}
