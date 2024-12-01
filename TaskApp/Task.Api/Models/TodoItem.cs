namespace Task.Api.Models;

public class TodoItem
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Descripition { get; set; }

    public bool IsComplete { get; set; }
}