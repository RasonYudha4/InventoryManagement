using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class Supplier : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = [];
}