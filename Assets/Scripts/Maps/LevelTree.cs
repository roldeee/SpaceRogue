using System;

public class LevelTree
{
    public LevelTreeNode root
    {
        get;
        private set;
    } = null;

    public LevelTree(string root)
    {
        this.root = new LevelTreeNode(root);
    }
}

public class LevelTreeNode
{
    public string value;

    public LevelTreeNode d1, d2, d3, d4;


    public LevelTreeNode(string value) {
        this.value = value;
    }

    public LevelTreeNode AddRoom(int door, string value) {
        if (door == 1)
        {
            return d1 = new LevelTreeNode(value);
        }
        else if (door == 2)
        {
            return d2 = new LevelTreeNode(value);
        }
        else if (door == 3)
        {
            return d3 = new LevelTreeNode(value);
        }
        else if (door == 4)
        {
            return d4 = new LevelTreeNode(value);
        } else
        {
            return null;
        }
    }
}