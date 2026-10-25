namespace Skvaller.Models;

public class Topic
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<Post> Posts { get; set; } = new();
}
