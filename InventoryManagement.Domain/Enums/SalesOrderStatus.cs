namespace InventoryManagement.Domain.Enums;

public enum SalesOrderStatus
{
    Draft = 1,
    Confirmed = 2,
    PartiallyDispatched = 3,
    Dispatched = 4,
    Cancelled = 5
}