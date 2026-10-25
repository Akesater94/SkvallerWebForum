namespace SkvallerAPI.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string AuthorId { get; set; } = string.Empty;
    public User? Author { get; set; }
    public int TopicId { get; set; }
    public Topic? Topic { get; set; }
}
