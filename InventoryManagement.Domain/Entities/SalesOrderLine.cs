using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class SalesOrderLine : BaseEntity
{
    public int LineNumber { get; set; }

    public int OrderedQuantity { get; set; }
    public int DispatchedQuantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => OrderedQuantity * UnitPrice;
    public DateTime? ExpectedShipDate { get; set; }

    // Navigation
    public Guid SalesOrderId { get; set; }
    public SalesOrder SalesOrder { get; set; } = null!;

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}