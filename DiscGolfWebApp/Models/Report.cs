using Skvaller.Data;

namespace Skvaller.Models;

public class Report
{
    public int Id { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsHandled { get; set; }

    public int PostId { get; set; }
    public Post? Post { get; set; }

    public string ReporterId { get; set; }
    public ApplicationUser? Reporter { get; set; }
}
