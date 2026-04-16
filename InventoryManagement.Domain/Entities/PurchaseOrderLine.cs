// Domain/Entities/PurchaseOrderLine.cs
using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class PurchaseOrderLine : BaseEntity
{
    public int LineNumber { get; set; }
    
    public int OrderedQuantity { get; set; }
    public int ReceivedQuantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalCost => OrderedQuantity * UnitCost;

    // Navigation
    public Guid PurchaseOrderId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; } = null!;

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}