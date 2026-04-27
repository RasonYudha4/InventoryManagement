using InventoryManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.Products.Queries;

public class GetProductByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetProductByIdQuery, ProductDetailDto>
{
    public async Task<ProductDetailDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.Id == request.Id && p.IsActive)
            .Select(p => new ProductDetailDto(
                p.Id,
                p.SKU,
                p.Name,
                p.Description,
                p.UnitCost,
                p.SellingPrice,
                p.ReorderPoint,
                p.ReorderQuantity,
                p.Category.Name,
                p.Supplier.Name))
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
            throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");

        return product;
    }
}