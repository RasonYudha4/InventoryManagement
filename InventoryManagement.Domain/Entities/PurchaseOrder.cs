// Domain/Entities/PurchaseOrder.cs
using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Domain.Entities;

public class PurchaseOrder : BaseEntity
{
    public string PONumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Draft;
    
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }

    // Navigation
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public ICollection<PurchaseOrderLine> OrderLines { get; set; } = [];
}