using System.Net;

namespace Skvaller.Services;

public class PostApiService(HttpClient httpClient) : IPostApiService
{
    public async Task<List<PostResponse>?> GetPostsByTopicAsync(int topicId)
    {
        var response = await httpClient.GetAsync($"api/topics/{topicId}/posts");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<PostResponse>>();
    }
}
