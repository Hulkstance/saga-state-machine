using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController(IPublishEndpoint publishEndpoint) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrderRequest request)
    {
        var orderId = Guid.NewGuid();
            
        await publishEndpoint.Publish(new OrderSubmitted
        {
            OrderId = orderId,
            Total = request.Total,
            Email = request.Email
        });

        return Ok(new { OrderId = orderId });
    }
}

public class SubmitOrderRequest
{
    public required decimal Total { get; set; }
    public required string Email { get; set; }
}
