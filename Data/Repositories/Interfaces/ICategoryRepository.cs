using WebApplication1.Models;

namespace WebApplication1.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(Category category);
        Task<List<Category>> GetAllCategoriesAsync();
    }
}
