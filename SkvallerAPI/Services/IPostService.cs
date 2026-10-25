using SkvallerAPI.Responses;

namespace SnackisApi.Services;

public interface IPostService
{
    Task<List<PostResponse>?> GetPostsByTopicAsync(int topicId);
}