using System.Collections;

public class PseudoRandom : System.Collections.IEnumerable
{
    private int _x;
    private int _a;
    private int _b;
    private int _m;
    private int _cycleLength = 10;

    private PseudoRandom(int intialValue, int a, int b, int m)
    {
        _x = intialValue;
        _a =a; _b = b; _m = m;
    }

    public int GetNext()
    {
        _x = (_a * _x + _b) % _m;
        return _x;
    }
    
    public static PseudoRandom PseudoRandomFactory10(int initialValue)
    {
        return new PseudoRandom(initialValue, 7, 5, 11);
    }

    public IEnumerator GetEnumerator()
    {
        return new PseudoRandomEnumerator(this);
    }

    public class PseudoRandomEnumerator : System.Collections.IEnumerator
    {
        private PseudoRandom _random;
        private int _position = -1;
        private int _cycleLength = 10;

        public PseudoRandomEnumerator(PseudoRandom random)
        {
            _random = random;
        }

        public object Current => _random.GetNext();
        

        public bool MoveNext()
        {
            _position++;
            return _position < _cycleLength;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}

public static class PseudoRandomTestClient
{
    public static void Test1()
    {
        var random = PseudoRandom.PseudoRandomFactory10(10);
        foreach(var item in random)
        {
            System.Console.Write($"{item}, ");
        }

        System.Console.WriteLine("\n==NEXT CYCLE==");
        foreach(var item in random)
        {
            System.Console.Write($"{item}, ");
        }
        var random1 =PseudoRandom.PseudoRandomFactory10(0);
        System.Console.WriteLine("\n==START FROM 0 CYCLE==");
        foreach(var item in random1)
            System.Console.Write($"{item}, ");
    }
}