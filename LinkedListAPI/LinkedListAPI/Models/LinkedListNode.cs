namespace LinkedListAPI.Models;

public class LinkedListNode
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public LinkedListNode Next { get; set; }

    public LinkedListNode(string value)
    {
        Id = Guid.NewGuid();
        Value = value;
        Next = null;
    }
}
