using Skvaller.Data;
using Skvaller.Repositories;

namespace Skvaller.Services;

public class ProfileService(IUserRepository userRepository, IWebHostEnvironment env) : IProfileService
{
    private const long MaxFileSize = 2 * 1024 * 1024;

    public async Task<ApplicationUser?> GetUserAsync(string userId)
    {
        return await userRepository.GetByIdAsync(userId);
    }
    public async Task<string?> SaveProfileImageAsync(string userId, Stream content, string originalFileName, long length)
    {
        var lowerName = originalFileName.ToLower();
        if (!(lowerName.EndsWith(".jpg") || lowerName.EndsWith(".jpeg") || lowerName.EndsWith(".png") || lowerName.EndsWith(".gif")))
        {
            return "Bilden måste vara en jpg, jpeg, png eller gif.";
        }

        if (length > MaxFileSize)
        {
            return "Bilden får vara högst 2 MB.";
        }

        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return "Användaren hittades inte.";
        }

        string fileName = Random.Shared.Next(0, 100000).ToString() + "_" + originalFileName;
        string folderPath = env.WebRootPath + "/profileImages/";

        using (var fileStream = new FileStream(folderPath + fileName, FileMode.Create))
        {
            await content.CopyToAsync(fileStream);
        }

        if (!string.IsNullOrEmpty(user.ImgPath))
        {
            string oldFile = folderPath + user.ImgPath;
            if (File.Exists(oldFile))
            {
                File.Delete(oldFile);
            }
        }

        user.ImgPath = fileName;
        await userRepository.UpdateAsync(user);
        return null;

    }
}
