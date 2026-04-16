using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var skuExist = await context.Products.AnyAsync(p => p.SKU == request.SKU, cancellationToken);
        if (skuExist)
        {
            throw new Exception($"Product with SKU {request.SKU} already exist");
        }

        var product = new Product
        {
            SKU = request.SKU,
            Name = request.Name,
            Description = request.Description,
            UnitCost = request.UnitCost,
            SellingPrice = request.SellingPrice,
            ReorderPoint = request.ReorderPoint,
            ReorderQuantity = request.ReorderQuantity,
            CategoryId = request.CategoryId,
            SupplierId = request.SupplierId,
            IsActive = true
        };

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}