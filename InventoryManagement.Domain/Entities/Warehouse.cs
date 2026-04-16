using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class Warehouse : BaseEntity
{
    public string Code { get; set; } = string.Empty;    // e.g., "NY-01"
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<Location> Locations { get; set; } = [];
}