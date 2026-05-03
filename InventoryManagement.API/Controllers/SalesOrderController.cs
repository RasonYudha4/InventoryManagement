using InventoryManagement.API.Models.Requests;
using InventoryManagement.Application.Features.SalesOrders.Commands.ConfirmSalesOrder;
using InventoryManagement.Application.Features.SalesOrders.Commands.CreateSalesOrder;
using InventoryManagement.Application.Features.SalesOrders.Queries.GetAllSalesOrders;
using InventoryManagement.Application.Features.SalesOrders.Queries.GetSalesOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalesOrderController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> CreateDraft([FromBody] CreateSalesOrderRequest request)
    {
        var command = new CreateSalesOrderCommand(
            request.CustomerName,
            request.CustomerContact,
            request.Notes,
            request.Items.Select(i => new SalesOrderLineItemDto(
                i.ProductId,
                i.Quantity,
                i.UnitPrice,
                i.ExpectedShipDate
            )).ToList()
        );

        var soId = await mediator.Send(command);
        return Ok(new { Message = "Draft Sales Order created.", Id = soId });
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}/confirm")]
    public async Task<IActionResult> ConfirmOrder(Guid id)
    {
        var command = new ConfirmSalesOrderCommand(id);
        await mediator.Send(command);
        return Ok(new { Message = "Sales Order confirmed successfully." });
    }

    [Authorize(Roles = "Admin,Manager,Auditor")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSalesOrderById(Guid id)
    {
        var query = new GetSalesOrderByIdQuery(id);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager,Auditor")]
    [HttpGet]
    public async Task<IActionResult> GetAllSalesOrders()
    {
        var query = new GetAllSalesOrdersQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}