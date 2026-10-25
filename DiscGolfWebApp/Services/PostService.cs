using Skvaller.Data;
using Skvaller.Models;
using Skvaller.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Skvaller.Services;

public class PostService : IPostService
{
    private readonly IPostRepository postRepository;
    private readonly ICategoryRepository categoryRepository;

    public PostService(IPostRepository postRepository, ICategoryRepository categoryRepository)
    {
        this.postRepository = postRepository;
        this.categoryRepository = categoryRepository;
    }

    public async Task<Topic?> GetTopicAsync(int id)
    {
        return await categoryRepository.GetTopicByIdAsync(id);
    }

    public async Task<Post?> GetPostAsync(int postId)
    {
        return await postRepository.GetByIdAsync(postId);
    }

    public async Task<List<Post>> GetPostsByTopicAsync(int topicId)
    {
        return await postRepository.GetByTopicAsync(topicId);
    }

    public async Task CreatePostAsync(int topicId, string authorId, string title, string content)
    {

        var newPost = new Post()
        {
            TopicId = topicId,
            AuthorId = authorId,
            Title = title,
            Content = content,
            CreatedAt = DateTime.Now
        };
    
        await postRepository.AddAsync(newPost);

    }

    public async Task<bool> DeletePostAsync(int postId, string userId, bool isAdmin)
    {
        var post = await postRepository.GetByIdAsync(postId);
        if (post is null)
        {
            return false;
        }

        if (!isAdmin && post.AuthorId != userId)
        {
            return false;
        }

        await postRepository.DeleteAsync(post);
        return true;
    }
}
