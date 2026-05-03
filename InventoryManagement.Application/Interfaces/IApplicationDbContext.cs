using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<Product> Products { get; }
    DbSet<Warehouse> Warehouses { get; }
    DbSet<Location> Locations { get; }
    DbSet<StockLevel> StockLevels { get; }
    DbSet<PurchaseOrder> PurchaseOrders { get; }
    DbSet<SalesOrder> SalesOrders { get; }
    DbSet<StockTransaction> StockTransactions { get; }
    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}