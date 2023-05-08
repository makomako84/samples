public static class StackTestClient
{
	public static void Run()
	{
		CustomStackExample();
	}
	
	private static void CustomStackExample()
	{
		var customStack = new CustomStack<int>(2);
		
		customStack.Push(2);
		customStack.Push(3);
		foreach(var item in customStack)
		{
			System.Console.WriteLine(item);
		}
		try
		{
			customStack.Push(5);
		}
		catch(CustomStackRuntimeException e)
		{
			System.Console.WriteLine(e.Message);
		}
	}
}

public class CustomStack<T> : IEnumerable<T>
{
	private T[] _arr;
	private int _current;
	
	public CustomStack(int length)
	{
		_arr = new T[length];
		_current = -1;
	}
	
	public void Push(T value)
	{
		if((_current + 1) == _arr.Length)
		{
			throw new CustomStackRuntimeException("Stack is full");
		}
		_current++;
		_arr[_current] = value;
	}
	public IEnumerator<T> GetEnumerator()
	{
		return ((IEnumerable<T>)_arr).GetEnumerator();
	}
	
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return _arr.GetEnumerator();
	}
}

public class CustomStackRuntimeException : System.Exception
{
	public CustomStackRuntimeException() : base() {}
	public CustomStackRuntimeException(string message) : base(message) {}
}