using Skvaller.Models;

namespace Skvaller.Services;

public interface IReportService
{
    Task<List<Report>> GetUnhandledReportsAsync();
    Task MarkAsHandledAsync(int reportId);
    Task<bool> ReportPostAsync(int postId, string reporterId, string reason);
}