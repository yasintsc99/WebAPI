using MongoDB.Driver;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class PostService : IPostService
    {
        private readonly IMongoCollection<Post> _postCollection;
        public PostService(IMongoDatabase mongoDatabase)
        {
            _postCollection = mongoDatabase.GetCollection<Post>("Post");
        }
        public async Task<List<Post>> GetAllPosts()
        {
            return await _postCollection.Find(x => x.isDeleted == false).ToListAsync();
        }
        public int GetCollectionCount()
        {
            return _postCollection.AsQueryable().Count();
        }
        public bool isThere(int id)
        {
            return _postCollection.Find(x => x.PostID == id).Any();
        }
        public async Task<Post> GetPostByID(int id)
        {
            return await _postCollection.Find(x => (x.PostID == id) && (x.isDeleted == false)).FirstOrDefaultAsync();
        }
        public async Task CreatePost(Post post)
        {
            post.PostID = _postCollection.AsQueryable().Count() + 1;
            await _postCollection.InsertOneAsync(post);
        }
        public async Task DeletePost(int id)
        {
            var deletePost = Builders<Post>.Update.Set("isDeleted", true);
            await _postCollection.UpdateOneAsync(x => x.PostID == id, deletePost);
        }

        public async Task UpdatePost(Post post)
        {
            var update = Builders<Post>.Update.Set("Title", post.Title)
                .Set("Description", post.Description)
                .Set("Content", post.Content)
                .Set("CategoryID", post.CategoryID);
            await _postCollection.UpdateOneAsync(x => x.PostID == post.PostID, update);
        }
    }
}
