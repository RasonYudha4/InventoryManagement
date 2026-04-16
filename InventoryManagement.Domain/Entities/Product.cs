using System.ComponentModel;
using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class Product : BaseEntity
{
    public string SKU {get; set;} = string.Empty;
    public string Name {get; set;} = string.Empty;
    public string? Description {get; set;}
    public decimal UnitCost {get; set;}
    public decimal SellingPrice {get; set;}
    public int ReorderPoint {get; set;}
    public int ReorderQuantity {get; set;}
    public string? Barcode {get; set;}
    public string? ImageUrl {get; set;}
    public bool IsActive {get; set;} = true;

    public Guid CategoryId {get; set;}
    public Category Category {get; set;} = null!;
    public Guid SupplierId {get; set;}
    public Supplier Supplier {get; set;} = null!;

    public ICollection<StockLevel> StockLevels {get; set;} = [];
    public ICollection<PurchaseOrderLine> PurchaseOrderLines {get; set;} = [];
    public ICollection<StockTransaction> Transactions {get; set;} = [];
    
}