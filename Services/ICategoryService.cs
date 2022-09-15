using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryByID(int id);
        int GetCollectionCount();

        bool isThere(int id);
        Task CreateCategory(Category category);
        Task DeleteCategory(int id);
        Task UpdateCategory(Category category);
    }
}
