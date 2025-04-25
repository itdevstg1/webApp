using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using WebApplication1.Data.Repositories.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly string _connectionString;

        public ItemRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnectionString");
        }
        public async Task AddItemAsync(Item item)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("AddItem", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Name", item.Name ?? "");
            cmd.Parameters.AddWithValue("@Price",item.Price ?? 0);
            cmd.Parameters.AddWithValue("@Description", item.Description ?? "");
            cmd.Parameters.AddWithValue("@CategoryId",item.CategoryId ?? (object)DBNull.Value);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
        public async Task UpdateItemAsync(Item item)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("UpdateItem", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@Name", item.Name ?? "");
            cmd.Parameters.AddWithValue("@Price", item.Price ?? 0);
            cmd.Parameters.AddWithValue("@Description", item.Description);
            cmd.Parameters.AddWithValue("@CategoryId", item.CategoryId);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

        }
        public async Task DeleteItemAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("DeleteItem", conn) { CommandType= CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

        }

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("GetItemById", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if(await reader.ReadAsync())
            {
                return new Item
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"]?.ToString(),
                    Price = reader["Price"] is DBNull ? null : (int?)reader["Price"],
                    Description = reader["Description"]?.ToString(),
                    CategoryId = reader["CategoryId"] is DBNull ? null : (int?)reader["CategoryId"]
                };
            }
            return null;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            var items = new List<Item>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("GetAllItems", conn) { CommandType= CommandType.StoredProcedure };
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while(await reader.ReadAsync())
            {
                items.Add(new Item
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"]?.ToString(),
                    Price = reader["Price"] is DBNull ? null : (int?)reader["Price"],
                    Description = reader["Description"]?.ToString(),
                    CategoryId = reader["CategoryId"] is DBNull ? null : (int?)reader["CategoryId"]
                }); 
            }
            return items;
        }
    }
}
