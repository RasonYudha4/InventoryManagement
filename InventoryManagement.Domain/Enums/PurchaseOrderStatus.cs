namespace InventoryManagement.Domain.Enums;

public enum PurchaseOrderStatus
{
    Draft = 1,
    PendingApproval = 2,
    Approved = 3,
    Rejected = 4,
    SentToSupplier = 5,
    PartiallyReceived = 6,
    Received = 7,
    Cancelled = 8
}