using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class StockLevel : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid LocationId { get; set; }
    public Location Location { get; set; } = null!;

    public int Quantity { get; set; }
    public int AllocatedQuantity { get; set; } // Reserved for outgoing orders but not yet shipped
    
    public int AvailableQuantity => Quantity - AllocatedQuantity;

    public DateTime? LastCountDate { get; set; } // Useful for Cycle Counting
}