using MediatR;

namespace InventoryManagement.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string SKU,
    string Name,
    string? Description,
    decimal UnitCost,
    decimal SellingPrice,
    int ReorderPoint,
    int ReorderQuantity,
    Guid CategoryId,
    Guid SupplierId
) : IRequest<Guid>;