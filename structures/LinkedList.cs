public static class LinkedListTest
{
	public static void TestCase1()
	{
		var item0 = new LinkedListNode();
		item0.Value = 4;
		var item1 = new LinkedListNode();
		item1.Value = 2;
		var item2 = new LinkedListNode();
		item2.Value = 3;
		
		var linkedList = new LinkedList();
		linkedList.Add(item0);
		linkedList.Add(item1);
		linkedList.Add(item2);
		
		
		linkedList.Iterate();

		var found = linkedList.Find(2);
		linkedList.Insert(found, new LinkedListNode(12));

		System.Console.WriteLine();
		linkedList.Iterate();
		System.Console.WriteLine();

		linkedList.DeleteAfter(found);
		
		System.Console.WriteLine();
		linkedList.Iterate();
		System.Console.WriteLine();
	}

	public static void TestCase2()
	{
		var linkedList = new LinkedList();
		linkedList.Add(new LinkedListNode(4));
		linkedList.Add(new LinkedListNode(44));
		linkedList.Add(new LinkedListNode(13));
		linkedList.Add(new LinkedListNode(22));
		linkedList.Add(new LinkedListNode(16));
		linkedList.Add(new LinkedListNode(15));
		linkedList.Add(new LinkedListNode(9));
		System.Console.WriteLine("INSERTION STRATEGY");
		Log(linkedList);
		linkedList.Find(13);
		Log(linkedList);
		linkedList.Find(15);
		Log(linkedList);
		linkedList.Find(9);
		Log(linkedList);
		linkedList.CurrentStrategy = SelfArrangeStrategy.Empty;
		System.Console.WriteLine("EMPTY STRATEGY");
		linkedList.Find(22);
		Log(linkedList);
		linkedList.Find(22);
		Log(linkedList);

		linkedList.CurrentStrategy = SelfArrangeStrategy.Swap;
		System.Console.WriteLine("Swap STRATEGY");
		linkedList.Find(22);
		Log(linkedList);
		linkedList.Find(22);
		Log(linkedList);	
	}

	public static void HasLoopExample()
	{
		var linkedList = new LinkedList();
		var item1 = new LinkedListNode(12);
		var item2 = new LinkedListNode(33);
		var item3 = new LinkedListNode(16);
		var item4 = new LinkedListNode(19);
		var item5 = new LinkedListNode(7);
		item5.Next = item3;
		linkedList.Add(item1);
		linkedList.Add(item2);
		linkedList.Add(item3);
		linkedList.Add(item4);
		linkedList.Add(item5);
		linkedList.Iterate(12);
		System.Console.WriteLine();
		System.Console.WriteLine($"Has loop: {linkedList.HasLoop()}");
		linkedList.Iterate(12);
		System.Console.WriteLine();
		System.Console.WriteLine($"Has loop: {linkedList.HasLoop()}");
	}

	public static void TestCase3()
	{
		var linkedList = new LinkedList();
		linkedList.Add(new LinkedListNode(4));
		linkedList.Add(new LinkedListNode(44));
		linkedList.Add(new LinkedListNode(13));
		linkedList.Add(new LinkedListNode(22));
		linkedList.Add(new LinkedListNode(16));
		linkedList.Add(new LinkedListNode(15));
		linkedList.Add(new LinkedListNode(9));

		Log(linkedList);

		linkedList.Sort();
		Log(linkedList);
	}

	public static void Log(LinkedList list)
	{
		list.Iterate();
		System.Console.WriteLine();
	}
	
}

public class LinkedListNode
{
	private int _value;
	public int Value 
	{ 
		get => _value; 
		set => _value = value;
	}
	public LinkedListNode Next;

	public LinkedListNode() {}
	public LinkedListNode(int value) { _value = value;}
	public LinkedListNode(LinkedListNode node)
	{
		_value = node.Value;
		Next = node.Next;
	}

}
public enum SelfArrangeStrategy
{
	Empty,
	Insertion,
	Swap
}

public class LinkedList
{
	public LinkedListNode Sentinel;
	public LinkedListNode Last;
	public SelfArrangeStrategy CurrentStrategy;
	
	public LinkedList()
	{
		Sentinel = new LinkedListNode();
		Last = Sentinel;
		CurrentStrategy = SelfArrangeStrategy.Insertion;
	}
	
	public void Add(LinkedListNode node)
	{
		Last.Next = node;
		Last = Last.Next;
		//реализация без Last
		//GetLast().Next = node;
	}
	
	public LinkedListNode GetLast()
	{
		var top = Sentinel;
		while(top.Next != null)
		{
			top = top.Next;
		}
		return top;
	}
	
	public void Iterate()
	{
		var top = Sentinel;
		while(top.Next != null)
		{
			System.Console.Write(top.Next.Value + ", ");
			top = top.Next;
		}
	}

	public void Iterate(int maxIterations)
	{
		int counter = 0;
		var top = Sentinel;
		while(top.Next != null && counter < maxIterations)
		{
			System.Console.Write(top.Next.Value + ", ");
			top = top.Next;
			counter++;
		}
	}
	
	public LinkedListNode Find(int target)
	{
		var current = Sentinel;
		LinkedListNode previous = Sentinel;
		
		while(current.Next != null)
		{
			if(current.Next.Value == target) 
			{
				switch(CurrentStrategy)
				{
					case SelfArrangeStrategy.Insertion:
						InsertStrategy(this, current, Sentinel);
						break;
					case SelfArrangeStrategy.Swap:
						SwapStrategy(this, current, previous);
						break;
				}

				return current.Next;
			}

			previous = current;
			current = current.Next;
		}
		return null;
	}

	public void Sort()
	{
		LinkedListNode oldSentinel = Sentinel;

		LinkedListNode newSentinel = new LinkedListNode();
		newSentinel.Next = null;

		oldSentinel = oldSentinel.Next; // now it points to first item

		while(oldSentinel != null)
		{
			LinkedListNode nextNode = oldSentinel;

			oldSentinel = oldSentinel.Next;

			LinkedListNode afterMe = newSentinel;
			while(afterMe.Next != null && afterMe.Next.Value < nextNode.Value)
			{
				afterMe = afterMe.Next;
			}

			nextNode.Next = afterMe.Next;
			afterMe.Next = nextNode;
		}
		Sentinel = newSentinel;
	}

	private static void InsertStrategy(LinkedList list,LinkedListNode current, LinkedListNode after)
	{
		var tmp = current.Next;
		list.DeleteAfter(current);
		tmp.Next = null;
		list.Insert(after, tmp);
	}

	private static void SwapStrategy(LinkedList list, LinkedListNode current, LinkedListNode sentinel)
	{
		if(sentinel.Next != current.Next)
		{
			var tmp = current.Next;
			list.DeleteAfter(current);
			tmp.Next = null;
			list.Insert(sentinel, tmp);
		}
	}

	public bool HasLoop()
	{
		var visited = new System.Collections.Hashtable();
		int index = 0;

		var current = Sentinel;
		while(current.Next != null)
		{
			if(visited.Contains(current.Next))
			{
				current.Next = null;
				return true;
			}

			visited.Add(current, current);
			current = current.Next;
			index++;
		}
		return false;
	}

	private void SwapWithNext(LinkedListNode current)
	{
		var tmp = current.Next.Value;;
		current.Next.Value = current.Value;
		current.Value = tmp;
	}

	public void Insert(LinkedListNode afterNode, LinkedListNode newNode)
	{
		newNode.Next = afterNode.Next;
		afterNode.Next = newNode;
	}

	public void DeleteAfter(LinkedListNode afterNode)
	{
		LinkedListNode target = afterNode.Next;
		afterNode.Next = target.Next;
	}
}
