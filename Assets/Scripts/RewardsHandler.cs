using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RewardsHandler : MonoBehaviour
{
    //private GameObject[] doors;
    public static string REWARDS_PATH = "Rewards/";
    public static string PREVIEW = "Preview";

    public GameObject[] rewardWaypoints;
    public GameObject currentRewardWaypoint;

    private PlayerDataManager playerDataManager;

    public enum Reward { HEALING, MAX_HEALTH_BOOST, SPREAD_FIRE, DOUBLE_FIRE, EXTRA_DASH, AUTO_RES };

    List<Reward> possibleRewards;

    // Start is called before the first frame update
    void Start()
    {
        playerDataManager = new PlayerDataManager();
        possibleRewards = new List<Reward>();
        foreach (Reward r in System.Enum.GetValues(typeof(Reward)))
        {
            possibleRewards.Add(r);
        }
    }

    public void showRewards()
    {
        playerDataManager.Load();
        List<string> rewardsUsed = new List<string>();
        Reward? nextReward = playerDataManager.playerData.nextReward;
        
        // Show current room reward
        if (nextReward.HasValue)
        {
            // Load reward prefab
            GameObject reward = Resources.Load<GameObject>(REWARDS_PATH + getRewardStr(playerDataManager.playerData.nextReward.Value));
            Instantiate(reward, currentRewardWaypoint.transform.position, Quaternion.identity);
            rewardsUsed.Add(reward.name);
        }

        // Show next rewards for each reward waypoint.
        foreach (GameObject waypoint in rewardWaypoints)
        {
            bool rewardChosen = false;
            do
            {
                // Choose random next reward that hasn't been used and create it.
                string rewardStr = getRewardStr(possibleRewards[UnityEngine.Random.Range(0, possibleRewards.Count)]);
                if (!rewardsUsed.Contains(rewardStr))
                {
                    rewardsUsed.Add(rewardStr);
                    GameObject reward = Resources.Load<GameObject>(REWARDS_PATH + PREVIEW + rewardStr); // Use preview reward prefabs.
                    Instantiate(reward, waypoint.transform.position, Quaternion.identity);
                    rewardChosen = true;
                }
            } while (rewardChosen == false);
        }
    }

    static public string getRewardStr(Reward reward)
    {
        switch (reward)
        {
            case Reward.HEALING:
                return "HealingReward";
            case Reward.MAX_HEALTH_BOOST:
                return "MaxHealthReward";
            case Reward.SPREAD_FIRE:
                return "SpreadFireReward";
            case Reward.DOUBLE_FIRE:
                return "DoubleFireReward";
            case Reward.EXTRA_DASH:
                return "ExtraDashReward";
            case Reward.AUTO_RES:
                return "AutoResReward";
            default:
                return "";
        }
    }

    static public Reward getRewardEnum(string reward)
    {
        switch (reward)
        {
            case "HealingReward":
                return Reward.HEALING;
            case "MaxHealthReward":
                return Reward.MAX_HEALTH_BOOST;
            case "SpreadFireReward":
                return Reward.SPREAD_FIRE;
            case "DoubleFireReward":
                return Reward.DOUBLE_FIRE;
            case "ExtraDashReward":
                return Reward.EXTRA_DASH;
            default:
                return Reward.AUTO_RES;
        }
    }
}
