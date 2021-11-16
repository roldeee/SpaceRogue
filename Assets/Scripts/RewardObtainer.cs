using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardObtainer : MonoBehaviour
{
    private PlayerDataManager playerDataManager;
    private Texture2DArray arr;
    private PlayerHealth playerHealth;
    private PlayerWeapon playerWeapon;
    private int healAmount = 1;

    private void Start()
    {
        playerDataManager = PlayerDataManager.Instance;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerWeapon = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<PlayerWeapon>();
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.tag.Equals("Player"))
        {
            playerDataManager.playerData.currentRewards.Add(RewardsHandler.GetRewardEnum(gameObject.name));
            // update player health
            if (gameObject.name.Contains("Healing"))
            {
                playerHealth.Heal(healAmount);
            }
            else if (gameObject.name.Contains("AutoRes"))
            {
                playerDataManager.playerData.hasAutoRes = true;
            }
            // update player shooting
            else if (gameObject.name.Contains("SpreadFire"))
            {
                playerWeapon.SetFireType(PlayerWeapon.FIRE_TYPE.SPREAD_FIRE);
            }
            else if (gameObject.name.Contains("DoubleFire"))
            {
                playerWeapon.SetFireType(PlayerWeapon.FIRE_TYPE.DOUBLE_FIRE);
            }
            // update player dashes
            else if (gameObject.name.Contains("ExtraDash"))
            {
                playerDataManager.playerData.dashCharges += 1;
            }

            Destroy(gameObject);
        }
    }
}
