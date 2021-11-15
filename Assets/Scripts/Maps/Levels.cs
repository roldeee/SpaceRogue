public class Levels
{
    public static LevelTree getLevel1()
    {
        LevelTree level1 = new LevelTree("SpawnScene");
        level1.root
            .AddRoom(1, "BasicRoom")
            .AddRoom(2, "BasicRoom")
            .AddRoom(3, "BasicRoom")
            .AddRoom(4, "BasicRoom")
            .AddRoom(2, "FinalScene");

        return level1;
    }
}
