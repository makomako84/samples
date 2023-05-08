using System.Collections;

public class ListStack  : System.Collections.IEnumerable
{
    private LinkedListNode _sentinel;
    public LinkedListNode Sentinel => _sentinel;

    public ListStack()
    {
        _sentinel = new LinkedListNode();
    }

    public void Push(int value)
    {
        var newNode = new LinkedListNode(value);
        newNode.Next = _sentinel.Next;
        _sentinel.Next = newNode;
    }

    public int Pop()
    {
        if(_sentinel.Next == null) throw new System.Exception("nothing to pop");
        int result = _sentinel.Next.Value;
        _sentinel.Next = _sentinel.Next.Next;   // связь с первым элементом (инстансом) обрывается здесь, его сожрет колектор
        return result;
    }

    public IEnumerator GetEnumerator()
    {
        return new ListStackEnumerator(this._sentinel);
    }
}

public class ListStackEnumerator : System.Collections.IEnumerator
{
    private LinkedListNode _current;

    public ListStackEnumerator(LinkedListNode sentinel)
    {
        _current = sentinel;
    }

    public object Current => _current;

    public bool MoveNext()
    {
        _current = _current.Next;
        return _current != null;
    }

    public void Reset()
    {
        _current = null;
    }
}



public static class StackTestClient1
{
    public static void TestListStack()
    {
        var listStack = new ListStack();
        listStack.Push(12);
        listStack.Push(24);
        listStack.Push(13);
        foreach(LinkedListNode item in listStack)
        {
            System.Console.WriteLine($"item: {item.Value}");
        }
        var value = listStack.Pop();
        System.Console.WriteLine($"poped value: {value}");
        foreach(LinkedListNode item in listStack)
        {
            System.Console.WriteLine($"item: {item.Value}");
        }
    }

    public static void ReverseArrayViaStack()
    {
        var arr = new int[] {1, 16, 24, 13, 3};
        foreach(var item in arr)
        {
            System.Console.WriteLine($"item: {item}");
        }
        System.Console.WriteLine("==========AFTER REVERSE==============");
        arr = ReverseArray(arr);
        foreach(var item in arr)
        {
            System.Console.WriteLine($"item: {item}");
        }
    }

    public static int[] ReverseArray(int[] arr)
    {
        var stack = new ListStack();
        var arrReversed = new int[arr.Length];
        for(int i=0; i < arr.Length; i++)
        {
            stack.Push(arr[i]);           
        }

        for(int i=0; i < arr.Length; i++)
        {
            arrReversed[i] = stack.Pop();
        }
        return arrReversed;
    }
}