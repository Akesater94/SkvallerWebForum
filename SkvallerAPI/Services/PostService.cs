using SkvallerAPI.Repositories;
using SkvallerAPI.Responses;
using SnackisApi.Services;

namespace SkvallerAPI.Services;

public class PostService(IPostRepository postRepository) : IPostService
{
    public async Task<List<PostResponse>?> GetPostsByTopicAsync(int topicId)
    {
        if (!await postRepository.TopicExistsAsync(topicId))
        {
            return null;
        }

        var posts = await postRepository.GetByTopicAsync(topicId);

        return posts.Select(p => new PostResponse
        {
            Id = p.Id,
            Title = p.Title,
            Content = p.Content,
            CreatedAt = p.CreatedAt,
            AuthorName = p.Author?.Name,
            AuthorId = p.AuthorId
        }).ToList();
    }
}
