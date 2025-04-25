using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebApplication1.Data.Repositories.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository) {
            _itemRepository = itemRepository;
        }

        public Task AddItemAsync(Item item) =>_itemRepository.AddItemAsync(item);

        public Task UpdateItemAsync(Item item) => _itemRepository.UpdateItemAsync(item);

        public Task DeleteItemAsync(int id) => _itemRepository.DeleteItemAsync(id);

        public Task<Item?> GetItemByIdAsync(int id) => _itemRepository.GetItemByIdAsync(id);

        public Task<List<Item>> GetAllItemsAsync() => _itemRepository.GetAllItemsAsync();
    }
}
