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
}
