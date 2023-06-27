namespace GroceryCartAPI.Models
{
    public class GroceryItem
    {
        public int? ItemId { get; set; }
        public string? ItemName { get; set; }

        public double? Price { get; set; }

        public int? Quantity { get; set; }
    }
}
