using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPosts();
        Task<Post> GetPostByID(int id);

        Task CreatePost(Post post);
        Task DeletePost(int id);

        bool isThere(int id);

        int GetCollectionCount();
        Task UpdatePost(Post post);
    }
}
