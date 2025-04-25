using WebApplication1.Models;

namespace WebApplication1.Services.Interfaces
{
    public interface IItemService
    {
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
        Task<Item?> GetItemByIdAsync(int id);
        Task<List<Item>> GetAllItemsAsync();
    }
}
