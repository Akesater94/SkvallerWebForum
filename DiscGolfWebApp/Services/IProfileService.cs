using Skvaller.Data;

namespace Skvaller.Services;

public interface IProfileService
{
    Task<ApplicationUser?> GetUserAsync(string userId);
    Task<string?> SaveProfileImageAsync(string userId, Stream content, string originalFileName, long length);
}
