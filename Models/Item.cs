using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int? Price { get; set; }
        public string? Description { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

    }
}
