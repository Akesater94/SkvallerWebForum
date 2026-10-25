using Skvaller.Models;
using Skvaller.Repositories;

namespace Skvaller.Services;

public class ReportService(IReportRepository reportRepository) : IReportService
{
    public async Task<bool> ReportPostAsync(int postId, string reporterId, string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
        {
            return false;
        }

        if (await reportRepository.CheckIfExistsAsync(postId, reporterId))
        {
            return false;
        }

        var newReport = new Report()
        {
            PostId = postId,
            ReporterId = reporterId,
            Reason = reason,
            CreatedAt = DateTime.Now,
            IsHandled = false
        };

        await reportRepository.AddAsync(newReport);

        return true;
    }

    public async Task<List<Report>> GetUnhandledReportsAsync()
    {
        return await reportRepository.GetUnhandledAsync();
    }

    public async Task MarkAsHandledAsync(int reportId)
    {
        var report = await reportRepository.GetByIdAsync(reportId);
        if (report == null)
        {
            return;
        }

        report.IsHandled = true;
        await reportRepository.UpdateAsync(report);
    }
}
