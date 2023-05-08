public class TreeNode
{
    public int Value { get; set; }
    public TreeNode LeftChild { get; set; }
    public TreeNode RightChild { get; set; }
    public TreeNode Parent { get; set; }
}
public enum TreeOrder
{
    Increase,
    Decrease
}
public class Tree
{
    private int[] _arr;
    int _currentIndex;
    public TreeOrder Increase { get; set; }

    public Tree(int height)
    {
        var length =((int)Math.Pow(2, height+1))-1;
        _arr = new int[length];
        _currentIndex = 0;
    }
    public void Add(int value)
    {
        _arr[_currentIndex] = value;
        _currentIndex++;
    }

    public void SortTree()
    {
        // сначала отсортирован уровень 0 (минимальный элемент)
        // затем отсортирован уровень 1
        // затем отсортирован уровень 2 итд.
        // находится минимальный элемент и выкидывается как можно ближе к основанию дерева
    }

    public int FindMinItem()
    {
        int minIndex = 0;
        for(int i=1; i < _arr.Length; i++)
        {
            if(_arr[i] < _arr[minIndex])
            {
                minIndex = i;
            }
        }
        return minIndex;
    }

    public (int, int) GetChilds(int index)
        => new (GetLeftChild(index), GetRightChild(index));

    public int GetLeftChild(int index)
        => _arr[2*index + 1];

    public int GetRightChild(int index)
        => _arr[2*index+2];
}

public static class TreeTestClient
{
    public static void TestCase1()
    {
        var tree = new Tree(2);
        tree.Add(4);
        tree.Add(16);
        tree.Add(7);
        tree.Add(22);
        tree.Add(13);
        tree.Add(14);
        tree.Add(2);
        /*
                 4
              16    7
            22 13 14 2
        */

        System.Console.WriteLine(tree.GetLeftChild(0));
        System.Console.WriteLine(tree.GetRightChild(0));
        System.Console.WriteLine(tree.GetLeftChild(1));
        System.Console.WriteLine(tree.GetLeftChild(2));
        System.Console.WriteLine(tree.GetRightChild(2));
        System.Console.WriteLine($"index 2: {tree.GetChilds(2)}");

    }
}