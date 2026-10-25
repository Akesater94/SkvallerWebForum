namespace SkvallerAPI.Models;

public class Topic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Post> Posts { get; set; } = new();
}
