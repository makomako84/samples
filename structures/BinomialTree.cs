public class BinomialNode
{
    public int Value { get; set; }
    public BinomialNode LeftChild { get; set; }
    public BinomialNode NextSibling { get; set; }
}

public class BinomialTree
{
    public BinomialNode RootSentinel { get; set; }

    public BinomialTree()
    {
        RootSentinel = new BinomialNode();
    }

    public void Add(int value)
    {
        
    }
}