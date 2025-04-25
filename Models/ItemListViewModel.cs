namespace WebApplication1.Models
{
    public class ItemListViewModel
    {
        public List<Item> Items { get; set; } = new();
        public Item NewItem { get; set; } = new();

        public List<Category> Category { get; set; } = new();
        public int TotalItemsInDatabase { get; set; }
        public int TotalItems { get; set; }
        public ItemFilter Filter { get; set; } = new();
    }
}
