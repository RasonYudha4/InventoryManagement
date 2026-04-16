using System.Security.Cryptography.X509Certificates;
using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Domain.Entities;

public class StockTransaction : BaseEntity
{
    public Guid ProductId {get; set;} 
    public Product Product {get; set;} = null!;
    public Guid LocationId {get; set;} 
    public Location Location {get; set;} = null!;
    public Guid? DestinationLocationId {get; set;}
    public Location? DestinationLocation {get; set;}

    public TransactionType Type {get; set;}
    public int Quantity {get; set;}
    public int QuantityBefore {get; set;}
    public int QuantityAfter {get; set;}
    public string? ReferenceNumber {get; set;}
    public string? Notes {get; set;}
    public string PerformedBy {get; set;} = string.Empty;
}