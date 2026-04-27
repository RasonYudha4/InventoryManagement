using System.ComponentModel;
using System.Text.Json;
using Microsoft.EntityFrameworkCore; 
using InventoryManagement.Infrastructure.Persistence;
using Microsoft.SemanticKernel; 

namespace InventoryManagement.Infrastructure.Services.AI.Plugins;

public class WarehousePlugin
{
    private readonly ApplicationDbContext _context;

    public WarehousePlugin(ApplicationDbContext context)
    {
        _context = context;
    }

    [KernelFunction("get_low_stock_items")]
    [Description("Gets a list of all products that currently have a stock level below their minimum threshold.")]
    public async Task<string> GetLowStockItemsAsync()
    {
        var items = await _context.Products
            .Where(p => p.StockLevels.Sum(sl => sl.Quantity) <= p.ReorderPoint)
            .Select(p => new 
            {
                p.SKU,
                p.Name,
                p.ReorderQuantity,
                p.SellingPrice
            })
            .ToListAsync();
            
        return JsonSerializer.Serialize(items);
    }

    [KernelFunction("get_all_products")]
    [Description("Gets a general list of all products currently available in the warehouse inventory.")]
    public async Task<string> GetAllProductsAsync()
    {
        var items = await _context.Products
            // Optional: If you have an IsDeleted flag like your background worker logs showed
            .Where(p => p.IsDeleted == false) 
            .Select(p => new 
            {
                p.SKU,
                p.Name,
                p.SellingPrice
            })
            .Take(50) // Safety net: Only grab the first 50 to avoid overloading the AI token limit
            .ToListAsync();
            
        return JsonSerializer.Serialize(items);
    }

    [KernelFunction("get_product_details")]
    [Description("Search for a specific product by its exact or partial name, or SKU, to get its full details, pricing, and total stock.")]
    public async Task<string> GetProductDetailsAsync(
        [Description("The name or SKU of the product to search for")] string searchTerm)
    {
        // 1. Search the database by Name or SKU
        var product = await _context.Products
            .Include(p => p.Category)      // Bring in the Category name
            .Include(p => p.Supplier)      // Bring in the Supplier name
            .Include(p => p.StockLevels)   // Bring in the actual stock counts
            .FirstOrDefaultAsync(p => 
                p.Name.Contains(searchTerm) || 
                p.SKU.Contains(searchTerm));

        // 2. Handle the "Not Found" scenario gracefully so the AI can tell the user
        if (product == null)
        {
            return $"Could not find any product matching '{searchTerm}'.";
        }

        // 3. Map it to an anonymous object to serialize. 
        // Notice how we SUM the StockLevels to get the true total!
        var details = new 
        {
            product.SKU,
            product.Name,
            product.Description,
            UnitCost = $"${product.UnitCost}",
            SellingPrice = $"${product.SellingPrice}",
            product.ReorderPoint,
            Category = product.Category?.Name ?? "Uncategorized",
            Supplier = product.Supplier?.Name ?? "Unknown Supplier",
            TotalStock = product.StockLevels.Sum(sl => sl.Quantity), // Enterprise stock calculation!
            product.IsActive
        };

        return JsonSerializer.Serialize(details);
    }
}