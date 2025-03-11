namespace shop_backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Description { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
