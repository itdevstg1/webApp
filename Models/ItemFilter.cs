namespace WebApplication1.Models
{
    public class ItemFilter
    {
        public string? Search {  get; set; }
        public string Order { get; set; } = "asc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
