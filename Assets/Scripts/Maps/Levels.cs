using System.Collections.Generic;
using UnityEngine;

public class Levels
{

    static Dictionary<string, int> rooms = new Dictionary<string, int>()
    {
        { "StartScene", 2 },
        { "BasicRoom", 3 },
        { "Scene4", 2 },
    };

    public static LevelTree getLevel1()
    {
        LevelTree level1 = new LevelTree("SpawnScene");
        LevelTreeNode r3 = level1.root
            .AddRoom(1, "StartScene")
            .AddRoom(1, "Scene4");

        r3.AddRoom(1, "BasicRoom")
            .AddRoom(3, "BasicRoom")
            .AddRoom(1, "FinalScene");

        r3.AddRoom(2, "BasicRoom")
            .AddRoom(2, "FinalScene");

        return level1;
    }

    // Strat: Hard coded start and final scenes
    // Have 10-15 rooms between
    // Phase 1: each room will have 1 room that it connects to
        // Pick random room to connect to, pick random egress from current room
    // Phase 2: splits
        // Use recursion for this? Each split would have it's own subtree?
    public static LevelTree getProceduralLevel()
    {
        LevelTree level = new LevelTree("SpawnScene");

        LevelTreeNode currRoom = level.root.AddRoom(1, "StartScene");

        string[] roomNames = new string[rooms.Keys.Count];
        rooms.Keys.CopyTo(roomNames, 0);

        int numRooms = Random.Range(10, 16);

        string currentRoomName = "StartScene";
        int currentNumEgresses = rooms[currentRoomName];

        int randomEgress;

        for (int i = 0; i <= numRooms; ++i)
        {
            int randomRoomIndex = Random.Range(0, roomNames.Length);
            string randomRomName = roomNames[randomRoomIndex];
            randomEgress = Random.Range(1, currentNumEgresses + 1);

            currRoom = currRoom.AddRoom(randomEgress, randomRomName);
            currentRoomName = randomRomName;
            currentNumEgresses = rooms[currentRoomName];
        }

        randomEgress = Random.Range(1, currentNumEgresses + 1);
        currRoom.AddRoom(randomEgress, "FinalScene");

        return level;
    }

    public static LevelTree GetProceduralLevel2()
    {
        string[] roomNames = new string[rooms.Keys.Count];
        rooms.Keys.CopyTo(roomNames, 0);

        LevelTree level = new LevelTree("SpawnScene");
        LevelTreeNode currRoom = level.root;

        // Add initial connecting room
        int randomRoomIndex = Random.Range(0, roomNames.Length);
        string randomRoomName = roomNames[randomRoomIndex];
        currRoom = currRoom.AddRoom(1, randomRoomName);


        int min = 10;
        int max = 15;

        FillPath(currRoom, roomNames, min, max);

        return level;
    }

    private static void FillPath(LevelTreeNode currentRoom, string[] roomNames, int min, int max)
    {
        // Get Egresses for current room
        int currentNumEgresses = rooms[currentRoom.value];

        // Pick random number of egresses that exist
        int numEgresses = Random.Range(1, currentNumEgresses + 1);

        // Get num between min and max
        if (min < 0) min = 0;
        if (max < 0) max = 0;
        int numRoomsLeft = Random.Range(min, max);

        // If numRoomsLeft is 0, add final room to random egress and break;
        if (numRoomsLeft == 0)
        {
            currentRoom.AddRoom(1, "FinalScene");
            return;
        }

        // If there are still rooms left in this path, add egress paths
        // For each egress, add room and fill its path
        for (int i = 1; i <= numEgresses; ++i)
        {
            int randomRoomIndex = Random.Range(0, roomNames.Length);
            string randomRoomName = roomNames[randomRoomIndex];
            LevelTreeNode room = currentRoom.AddRoom(i, randomRoomName);
            FillPath(room, roomNames, min - 1, max - 1);
        }
    }
}
