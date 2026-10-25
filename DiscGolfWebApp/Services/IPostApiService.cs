namespace Skvaller.Services;

public interface IPostApiService
{
    // Returnerar null om ämnet inte finns
    Task<List<PostResponse>?> GetPostsByTopicAsync(int topicId);
}
