public class Levels
{
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
}
