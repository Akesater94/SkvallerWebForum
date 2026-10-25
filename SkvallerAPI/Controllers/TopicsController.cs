using Microsoft.AspNetCore.Mvc;
using SkvallerAPI.Responses;
using SnackisApi.Services;

namespace SkvallerAPI.Controllers;

[ApiController]
[Route("api/topics")]
public class TopicsController(IPostService postService) : ControllerBase
{
    [HttpGet("{topicId:int}/posts")]
    public async Task<ActionResult<List<PostResponse>>> GetPosts(int topicId)
    {
        var posts = await postService.GetPostsByTopicAsync(topicId);

        if (posts == null)
        {
            return NotFound();
        }

        return Ok(posts);
    }
}