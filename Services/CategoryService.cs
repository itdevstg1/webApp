using WebApplication1.Data.Repositories.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Task AddCategoryAsync(Category category) => _categoryRepository.AddCategoryAsync(category);
        public Task<List<Category>> GetAllCategoriesAsync() => _categoryRepository.GetAllCategoriesAsync();
    }
}
