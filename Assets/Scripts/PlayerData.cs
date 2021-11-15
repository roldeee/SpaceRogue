using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int score = 0;
    public List<RewardsHandler.Reward> currentRewards = new List<RewardsHandler.Reward>();
    public int currentHealth = 10;
    public int maxHealth = 10;
    public RewardsHandler.Reward? nextReward = null;
    public int numRoomsCleared = 0;

    // Assume level 1 for simplicity
    public LevelTreeNode currentRoom = Levels.getLevel1().root;
}
