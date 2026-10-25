using Skvaller.Data;

namespace Skvaller.Models;

public class PrivateMessage
{
    public int Id { get; set; }
    public string Content { get; set; } = "";
    public DateTime SentAt { get; set; } = DateTime.Now;
    public bool IsRead { get; set; }

    public string SenderId { get; set; }
    public ApplicationUser? Sender { get; set; }

    public string ReceiverId { get; set; } = string.Empty;
    public ApplicationUser? Receiver { get; set; }

}
