using System;

public class Levels
{
    public static LevelTree getLevel1()
    {
        LevelTree level1 = new LevelTree("SpawnScene");
        level1.root
            .AddRoom(2, "Level2")
            .AddRoom(2, "FinalScene");

        return level1;
    }
}
