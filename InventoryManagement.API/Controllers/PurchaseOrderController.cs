using InventoryManagement.Application.Features.PurchaseOrders.Commands.CreatePurchaseOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class PurchaseOrderController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDraft([FromBody] CreatePurchaseOrderCommand command)
    {
        var poId = await mediator.Send(command);
        return Ok(new { Message = "Draft Purchase Order created.", Id = poId});
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}/approve")]
    public async Task<IActionResult> ApproveOrder(Guid id)
    {
        var command = new ApprovePurchaseOrderCommand(id);
        await mediator.Send(command);

        return Ok(new { Message = "Purchase Order Approved successfully." });
    }
}