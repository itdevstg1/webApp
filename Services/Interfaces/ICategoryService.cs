using WebApplication1.Models;

namespace WebApplication1.Services.Interfaces
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(Category category);
        Task<List<Category>> GetAllCategoriesAsync();
    }
}
