namespace Skvaller.Services;
using Skvaller.Models;
using Skvaller.Data;
using Microsoft.EntityFrameworkCore;
using Skvaller.Repositories;

public class CommentService : ICommentService
{
    private readonly ICommentRepository commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task<List<Comment>> GetCommentsByPostAsync(int postId)
    {
        return await commentRepository.GetByPostAsync(postId);
    }

    public async Task AddCommentAsync(int postId, string authorId, string content)
    {
        if (!string.IsNullOrWhiteSpace(content))
        {
            var newComment = new Comment()
            {
                PostId = postId,
                AuthorId = authorId,
                Content = content,
                CreatedAt = DateTime.Now
            };

            await commentRepository.AddAsync(newComment);
        }
    }

    public async Task<bool> DeleteCommentAsync(int commentId, string userId, bool isAdmin)
    {
        var comment = await commentRepository.GetByIdAsync(commentId);
        if (comment is null)
        {
            return false;
        }

        if (!isAdmin && comment.AuthorId != userId)
        {
            return false;
        }

        await commentRepository.DeleteAsync(comment);
        return true;
    }
}
