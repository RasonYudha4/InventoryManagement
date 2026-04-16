using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class Location : BaseEntity
{
    public string Code { get; set; } = string.Empty;     // e.g., "A-01-B"
    public string? Aisle { get; set; }
    public string? Rack { get; set; }
    public string? Shelf { get; set; }
    public string? Bin { get; set; }
    public decimal MaxWeightCapacity { get; set; }       // Enterprise constraint
    public bool IsActive { get; set; } = true;

    // Navigation
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;

    public ICollection<StockLevel> StockLevels { get; set; } = [];
}