using System.Data;
using Microsoft.Data.SqlClient;
using WebApplication1.Data.Repositories.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnectionString");
        }
        public async Task AddCategoryAsync(Category category)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("AddCategory", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Name", category.Name ?? "");
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = new List<Category>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("GetAllCategories", conn) { CommandType = CommandType.StoredProcedure };
            await conn.OpenAsync();
            using var reader = cmd.ExecuteReader();
            while (await reader.ReadAsync())
            {
                categories.Add(new Category
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"]?.ToString()
                });
            }
            return categories;
        }
    }
}
