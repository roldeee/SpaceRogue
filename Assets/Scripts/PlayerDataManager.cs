using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager
{
    private static PlayerDataManager instance = null;
    private static readonly object padlock = new object();

    public PlayerData playerData;
    private PlayerDataManager()
    {
        playerData = new PlayerData();
    }

    public static PlayerDataManager Instance { 
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new PlayerDataManager();
                }
                return instance;
            }
        }
    }
}
