using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerData
{
    public int score = 0;
    public List<RewardsHandler.Reward> currentRewards = new List<RewardsHandler.Reward>();
    public int currentHealth = 100;
    public int maxHealth = 100;
    public RewardsHandler.Reward? nextReward = null;
}
