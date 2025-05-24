namespace LinkedListAPI.Models;

public class LinkedList
{
    private LinkedListNode head;
    private LinkedListNode tail;

    public void Add(string value)
    {
        var newNode = new LinkedListNode(value);

        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            tail = newNode;
        }
    }

    public bool Remove(Guid id)
    {
        if (head == null) return false;

        if (head.Id == id)
        {
            head = head.Next;
            if (head == null) tail = null;
            return true;
        }

        var current = head;
        while (current.Next != null)
        {
            if (current.Next.Id == id)
            {
                current.Next = current.Next.Next;
                if (current.Next == null) tail = current;
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    public List<object> ToList()
    {
        var result = new List<object>();
        var current = head;

        while (current != null)
        {
            result.Add(new { Id = current.Id, Value = current.Value });
            current = current.Next;
        }

        return result;
    }
}
