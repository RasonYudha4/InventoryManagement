namespace InventoryManagement.UI.Models;

public class ProductDto
{
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int ReorderQuantity { get; set; }
    public decimal SellingPrice { get; set; }
}