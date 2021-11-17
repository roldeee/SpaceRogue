using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RewardsHandler : MonoBehaviour
{
    public static string REWARDS_PATH = "Rewards/";
    public static string PREVIEW = "Preview";

    public GameObject[] previewRewardWaypoints;
    public GameObject currentRewardWaypoint;

    private PlayerDataManager playerDataManager;

    public enum Reward { HEALING, SPREAD_FIRE, DOUBLE_FIRE, EXTRA_DASH, AUTO_RES };

    List<Reward> possibleRewards;

    // Start is called before the first frame update
    void Start()
    {
        playerDataManager = PlayerDataManager.Instance;
        possibleRewards = new List<Reward>();
        foreach (Reward r in System.Enum.GetValues(typeof(Reward)))
        {
            possibleRewards.Add(r);
        }
    }

    public void ShowRewards()
    {
        List<string> rewardsUsed = new List<string>();
        Reward? nextReward = playerDataManager.playerData.nextReward;

        // Show current room reward
        if (nextReward.HasValue)
        {
            // Load reward prefab
            GameObject reward = Resources.Load<GameObject>(REWARDS_PATH + GetRewardStr(playerDataManager.playerData.nextReward.Value));
            Instantiate(reward, currentRewardWaypoint.transform.position, Quaternion.identity);
            rewardsUsed.Add(reward.name);
        }
        // Show next rewards for each reward waypoint.
        foreach (GameObject waypoint in previewRewardWaypoints)
        {
            if (waypoint.activeInHierarchy)
            {
                bool rewardChosen = false;
                do
                {
                    // Choose random next reward that hasn't been used and create it.
                    string rewardStr = GetRewardStr(possibleRewards[UnityEngine.Random.Range(0, possibleRewards.Count)]);
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
    }

    static public string GetRewardStr(Reward reward)
    {
        switch (reward)
        {
            case Reward.HEALING:
                return "HealingReward";
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

    static public Reward GetRewardEnum(string reward)
    {
        switch (reward)
        {
            case "HealingReward":
                return Reward.HEALING;
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
