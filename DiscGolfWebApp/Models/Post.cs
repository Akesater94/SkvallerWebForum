using Skvaller.Data;

namespace Skvaller.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string AuthorId { get; set; }
    public ApplicationUser? Author { get; set; }

    public int TopicId { get; set; }
    public Topic? Topic { get; set; }

    public List<Comment> Comments { get; set; } = new();
}
