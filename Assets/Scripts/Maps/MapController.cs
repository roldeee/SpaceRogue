using UnityEngine;
using System.Collections;

/**
 *  A utility that controlls the different rooms and levels for a playthrough.
 *  
 *  Initially this will use pre-generated Maps, but could be enhanced to use
 *  procedurely generated maps.
 */
public class MapController : MonoBehaviour
{
    private PlayerData playerData;

    private void Start()
    {
        playerData = PlayerDataManager.Instance.playerData;
    }

    public LevelTreeNode GetCurrentRoom()
    {
        return playerData.currentRoom;
    }

    public LevelTreeNode GetNextRoom(int door)
    {
        switch (door)
        {
            case 1:
                if (playerData.currentRoom.d1 != null)
                {
                    playerData.currentRoom = playerData.currentRoom.d1;
                    return playerData.currentRoom;
                }
                return null;
            case 2:
                if (playerData.currentRoom.d2 != null)
                {
                    playerData.currentRoom = playerData.currentRoom.d2;
                    return playerData.currentRoom;
                }
                return null;
            case 3:
                if (playerData.currentRoom.d3 != null)
                {
                    playerData.currentRoom = playerData.currentRoom.d3;
                    return playerData.currentRoom;
                }
                return null;
            case 4:
                if (playerData.currentRoom.d4 != null)
                {
                    playerData.currentRoom = playerData.currentRoom.d4;
                    return playerData.currentRoom;
                }
                return null;
            default:
                return null;
        }
    }

    public void SetDoorVisibility()
    {
        GameObject door1 = GameObject.Find("Door1");
        GameObject door2 = GameObject.Find("Door2");
        GameObject door3 = GameObject.Find("Door3");

        GameObject wall1 = GameObject.Find("Wall1");
        GameObject wall2 = GameObject.Find("Wall2");
        GameObject wall3 = GameObject.Find("Wall3");

        GameObject waypoint1 = GameObject.Find("Waypoint1");
        GameObject waypoint2 = GameObject.Find("Waypoint2");
        GameObject waypoint3 = GameObject.Find("Waypoint3");

        LevelTreeNode currentRoom = GetCurrentRoom();

        // Door 1
        if (door1 != null && currentRoom.d1 == null)
        {
            door1.SetActive(false);
            wall1?.SetActive(true);
            waypoint1?.SetActive(false);
        }
        else if (door1 != null && wall1 != null)
        {
            door1.SetActive(true);
            wall1.SetActive(false);
        }

        // Door 2
        if (door2 != null && currentRoom.d2 == null)
        {
            door2.SetActive(false);
            wall2?.SetActive(true);
            waypoint2?.SetActive(false);
        }
        else if (door2 != null && wall2 != null)
        {
            door2.SetActive(true);
            wall2.SetActive(false);
        }

        // Door 3
        if (door3 != null && currentRoom.d3 == null)
        {
            door3.SetActive(false);
            wall3?.SetActive(true);
            waypoint3?.SetActive(false);
        }
        else if (door3 != null && wall3 != null)
        {
            door3.SetActive(true);
            wall3.SetActive(false);
        }
    }
}
