using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Reflection.Metadata.Ecma335;
using WebAPI.Models;
namespace WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Post> _postCollection;
        public CategoryService(IMongoDatabase mongoDatabase)
        {
            _categoryCollection = mongoDatabase.GetCollection<Category>("Category");
            _postCollection = mongoDatabase.GetCollection<Post>("Post");
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _categoryCollection.Find(x => x.isDeleted == false).ToListAsync(); ;
        }

        public async Task<Category> GetCategoryByID(int id)
        {
            return await _categoryCollection.Find(x => x.CategoryId == id && x.isDeleted == false).FirstOrDefaultAsync();
        }

        public bool isThere(int id)
        {
            return _categoryCollection.Find(x => x.CategoryId == id).Any();
        }
        public int GetCollectionCount()
        {
            return _categoryCollection.AsQueryable().Count();
        }
        public async Task CreateCategory(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);
        }
        public async Task DeleteCategory(int id)

        {
            var deletePost = Builders<Post>.Update.Set("isDeleted", true);
            var deleteCategory = Builders<Category>.Update.Set("isDeleted", true);
            if (_postCollection.Find(x => x.CategoryID == id).Any())
                await _postCollection.UpdateManyAsync(x => x.CategoryID == id, deletePost);
            await _categoryCollection.UpdateManyAsync(x => x.CategoryId == id, deleteCategory);
        }

        public async Task UpdateCategory(Category category)
        {
            await _categoryCollection.ReplaceOneAsync(x => x.CategoryId == category.CategoryId,category);
        }
    }
}
