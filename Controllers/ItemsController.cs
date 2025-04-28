using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    public class ItemsController : Controller
    {
        //private readonly MyAppContext _context;
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;
        public ItemsController(IItemService itemService,ICategoryService categoryService) {
            _itemService = itemService;
            _categoryService = categoryService;
        }
        /// <summary>
        /// Welcome page with list of items and categories
        /// Possibility to do pagination and search
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index([FromQuery] ItemFilter filter) 
        {
            var allItems = await _itemService.GetAllItemsAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();
            foreach (var item in allItems)
            {
                item.Category = categories.FirstOrDefault(c => c.Id == item.CategoryId);
            }

            var query = allItems.AsQueryable();
            var totalItemsInDatabase = allItems.Count();

            if (!string.IsNullOrWhiteSpace(filter.Search))

                query = query.Where(i => i.Name.ToLower().Contains(filter.Search));
                query = filter.Order == "desc" ? query.OrderByDescending(i => i.Id) : query.OrderBy(i =>  i.Id);
                var totalItems = query.Count();

                var items = query
                    .Skip((filter.Page - 1 ) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToList();

            var vm = new ItemListViewModel
            {
                Items = items,
                NewItem = new Item(),
                TotalItemsInDatabase = totalItemsInDatabase,
                TotalItems = totalItems,
                Filter = filter,
                Category = categories
            };

            return View(vm);
        }
        /// <summary>
        /// Add item 
        /// </summary>
        /// <param name="NewItem"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Create(Item NewItem) 
        {
            if(ModelState.IsValid)
            {
                await _itemService.AddItemAsync(NewItem);
                TempData["ToastMessage"] = $"{NewItem.Name} created successfully!";
                return RedirectToAction(nameof(Index));
            }
            //si le formulaire n'est pas valide on recharge les donnees 
            var vm = new ItemListViewModel
            {
                Items = await _itemService.GetAllItemsAsync(),
                NewItem = new Item(),
                Category = await _categoryService.GetAllCategoriesAsync()
            };
            return View("Index",vm);
        }

        /// <summary>
        /// Edition Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                await _itemService.UpdateItemAsync(item);
            }
            TempData["ToastMessage"] = $"{item.Name } updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if(item == null) {
                return NotFound();
            }
            await _itemService.DeleteItemAsync(id);
            TempData["ToastMessage"] = $"{item.Name} deleted succesfully ! ";
            return RedirectToAction(nameof(Index));
        }

    }
}
