using MediatR;

namespace InventoryManagement.Application.Features.Stock.Queries.GetStockLevelsByLocation;

public record StockLevelDto(
    Guid ProductId,
    string SKU,
    string ProductName,
    int TotalQuatity,
    int AllocatedQuantity,
    int AvailableQuantity
);

public record GetStockLevelsByLocationQuery(Guid LocationId) : IRequest<List<StockLevelDto>>;