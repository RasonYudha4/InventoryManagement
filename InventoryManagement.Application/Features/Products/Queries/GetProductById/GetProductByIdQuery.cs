using MediatR;
namespace InventoryManagement.Application.Features.Products.Queries;

public record ProductDetailDto(
    Guid Id,
    string SKU,
    string Name,
    string? Description,
    decimal UnitCost,
    decimal SellingPrice,
    int ReorderPoint,
    int ReorderQuantity,
    string CategoryName,
    string SupplierName
);

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDetailDto>;