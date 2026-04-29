using InventoryManagement.API.Models.Requests;
using InventoryManagement.Application.Features.Products.Commands.CreateProduct;
using InventoryManagement.Application.Features.Products.Queries;
using InventoryManagement.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var command = new CreateProductCommand(
            request.SKU,
            request.Name,
            request.Description,
            request.UnitCost,
            request.SellingPrice,
            request.ReorderPoint,
            request.ReorderQuantity,
            request.CategoryId,
            request.SupplierId
        );

        var productId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetProductById), new { id = productId }, new { Id = productId });
    }

    [Authorize(Roles = "Admin,Manager,WarehouseStaff,Auditor")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
    var result = await mediator.Send(new GetProductByIdQuery(id));
    return Ok(result);
    }
    
    [Authorize(Roles = "Admin,Manager,WarehouseStaff,Auditor")]
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await mediator.Send(new GetAllProductsQuery());
        return Ok(result);
    }
}