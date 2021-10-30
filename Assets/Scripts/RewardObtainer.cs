using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardObtainer : MonoBehaviour
{
    private PlayerDataManager playerDataManager;

    private void Start()
    {
        playerDataManager = new PlayerDataManager();
        playerDataManager.Load();
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.tag.Equals("Player"))
        {
            playerDataManager.playerData.currentRewards.Add(RewardsHandler.getRewardEnum(gameObject.name));
            playerDataManager.Save();
            Debug.Log("RewardObtainer: Logging data");
            playerDataManager.LogData();
        }
        Destroy(gameObject);
    }
}
