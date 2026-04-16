// Domain/Enums/UserRole.cs
namespace InventoryManagement.Domain.Enums;

public enum UserRole
{
    Admin = 1,          // Full system access
    Manager = 2,        // Can approve POs, view full reports
    WarehouseStaff = 3, // Can receive, transfer, and pick stock
    Auditor = 4         // Read-only access to transactions and logs
}