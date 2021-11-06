using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardObtainer : MonoBehaviour
{
    private PlayerDataManager playerDataManager;
    private Texture2DArray arr;

    private void Start()
    {
        playerDataManager = PlayerDataManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.tag.Equals("Player"))
        {
            playerDataManager.playerData.currentRewards.Add(RewardsHandler.getRewardEnum(gameObject.name));
        }
        Destroy(gameObject);
    }
}
