using WebApplication1.Models;

namespace WebApplication1.Data.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
        Task<Item?> GetItemByIdAsync(int id);
        Task<List<Item>> GetAllItemsAsync();
    }
}
