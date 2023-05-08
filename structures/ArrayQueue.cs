using System.Collections;

public class ArrayQueue : System.Collections.IEnumerable
{
    private int[] _arr;
    private int _next = 0;
    private int _last = 0;

    public int Next => 0;
    public int Last => 0;
    public int[] Arr => _arr;

    public int QueueSize => _arr.Length;

    public ArrayQueue(int size)
    {
        _arr = new int[size];
    }

    public void Enqueue(int value)
    {
        if((_next + 1) % QueueSize == _last) throw new System.Exception("full");

        _arr[_next] = value;
        _next = (_next+1) % QueueSize;
    }

    public int Dequeue()
    {
        if(_next == _last) throw new System.Exception("empty");

        int value = _arr[_last];
        _last = (_last + 1) % QueueSize;

        return value;
    }

    public IEnumerator GetEnumerator()
    {
        return new QueueEnumerator(this._arr, _next);
    }
}

public class QueueEnumerator : System.Collections.IEnumerator
{
    private int[] _queue;
    int _currentIndex = -1;
    int _next;
    public QueueEnumerator(int[] arr, int next)
    {
        _queue = arr;
        _next = next;
    }

    public object Current => _queue[_currentIndex];

    public bool MoveNext()
    {
        _currentIndex++;
        return _currentIndex < _next;
    }

    public void Reset()
    {
        _currentIndex = -1;
    }
}

public static class ArrQueueTest
{
    public static void TestCase1()
    {
        var queue = new ArrayQueue(6);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(13);
        queue.Enqueue(13);
        queue.Enqueue(11);

        foreach(int item in queue)
        {
            System.Console.WriteLine(item);
        }

        queue.Dequeue();
        queue.Dequeue();
        queue.Dequeue();
        System.Console.WriteLine("after half deququuqe");
        foreach(int item in queue)
        {
            System.Console.WriteLine(item);
        }
        queue.Dequeue();
        queue.Dequeue();
        System.Console.WriteLine("after deququuqe");
        foreach(int item in queue)
        {
            System.Console.WriteLine(item);
        }
    }
}