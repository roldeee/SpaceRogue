using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomClearChecker : MonoBehaviour
{
    public static RewardsHandler rewardHandler;
    private int numEnemies;
    bool isRoomCleared = false;

    private void Start()
    {
        rewardHandler = GetComponent<RewardsHandler>();
    }

    private void Update()
    {
        if (!isRoomCleared)
        {
            CheckRoomCleared();
        }
    }

    public void CheckRoomCleared()
    {
        if (numEnemies == 0)
        {
            rewardHandler.ShowRewards();
            isRoomCleared = true;
        };
    }

    public bool IsRoomCleared()
    {
        return isRoomCleared;
    }

    public void setNumEnemies(int count)
    {
        numEnemies = count;
        //Debug.Log("NUM ENEMIES: " + count);
        // Check if room was set to 0 enemies
        if (numEnemies == 0)
        {
            isRoomCleared = true;
        }
    }

    public void RemoveEnemy()
    {
        numEnemies--;
        //Debug.Log("Num enemies remaining: " + numEnemies);
        if (numEnemies < 0)
        {
            throw new System.Exception("Number of enemies is negative.");
        }
        CheckRoomCleared();
    }
}
