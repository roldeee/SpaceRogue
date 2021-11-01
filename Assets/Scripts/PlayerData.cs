using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int score = 0;
    public List<RewardsHandler.Reward> currentRewards = new List<RewardsHandler.Reward>();
    public int currentHealth = 5;
    public int maxHealth = 5;
    public RewardsHandler.Reward? nextReward = null;
    public int numRoomsCleared = 0;
}
