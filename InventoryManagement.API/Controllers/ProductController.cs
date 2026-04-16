using InventoryManagement.Application.Features.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IMediator mediator) : ControllerBase
{
    // Only Admins and Managers can add new products to the catalog
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        // MediatR takes the body, validates the pricing rules, and saves to the DB
        var productId = await mediator.Send(command);
        
        return CreatedAtAction(nameof(GetProductById), new { id = productId }, new { Id = productId });
    }

    // Everyone logged into the system can view products
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        // TODO: We will build the GetProductByIdQuery next!
        return Ok($"Product {id} retrieved.");
    }
}