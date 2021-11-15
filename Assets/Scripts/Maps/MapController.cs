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
    LevelTree level = null;
    LevelTreeNode currentRoom = null;

    private void Awake()
    {
        // Assume we're using Level 1 for simplicity
        level = Levels.getLevel1();
        currentRoom = level.root;
    }

    public LevelTreeNode getNextRoom(int door)
    {
        switch (door)
        {
            case 1:
                if (currentRoom.d1 != null)
                {
                    currentRoom = currentRoom.d1;
                    return currentRoom;
                }
                return null;
            case 2:
                if (currentRoom.d2 != null)
                {
                    currentRoom = currentRoom.d2;
                    return currentRoom;
                }
                return null;
            case 3:
                if (currentRoom.d3 != null)
                {
                    currentRoom = currentRoom.d3;
                    return currentRoom;
                }
                return null;
            case 4:
                if (currentRoom.d4 != null)
                {
                    currentRoom = currentRoom.d4;
                    return currentRoom;
                }
                return null;
            default:
                return null;
        }
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
