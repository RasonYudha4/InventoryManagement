using InventoryManagement.API.Models.Requests;
using InventoryManagement.Application.Features.PurchaseOrders.Commands.CreatePurchaseOrder;
using InventoryManagement.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;
using InventoryManagement.Application.Features.PurchaseOrders.Queries.GetPurchaseOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PurchaseOrderController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> CreateDraft([FromBody] CreatePurchaseOrderRequest request)
    {
        var command = new CreatePurchaseOrderCommand(
            request.SupplierId,
            request.Notes,
            request.Items.Select(i => new PurchaseOrderLineItemDto(
                i.ProductId,
                i.Quantity,
                i.NegotiatedUnitCost,
                i.ExpectedDeliveryDate
            )).ToList()
        );

        var poId = await mediator.Send(command);
        return Ok(new { Message = "Draft Purchase Order created.", Id = poId });
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}/approve")]
    public async Task<IActionResult> ApproveOrder(Guid id)
    {
        var command = new ApprovePurchaseOrderCommand(id);
        await mediator.Send(command);
        return Ok(new { Message = "Purchase Order approved successfully." });
    }

    [Authorize(Roles = "Admin,Manager,Auditor")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPurchaseOrderById(Guid id)
    {
        var query = new GetPurchaseOrderByIdQuery(id);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager,Auditor")]
    [HttpGet]
    public async Task<IActionResult> GetAllPurchaseOrders()
    {
        var query = new GetAllPurchaseOrdersQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}