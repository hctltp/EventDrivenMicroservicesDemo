using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Models;
using SharedEvents;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        // Simulate order creation (e.g., save to DB)
        var orderId = Guid.NewGuid();

        var order = new Order
        {
            Id = orderId,
            CustomerName = dto.CustomerName,
            CreatedAt = DateTime.UtcNow
        };

        using (var scope = HttpContext.RequestServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
        }

        await _publishEndpoint.Publish(new OrderCreatedEvent
        {
            OrderId = orderId,
            CustomerName = dto.CustomerName,
            CreatedAt = DateTime.UtcNow
        });

        return Ok(new { OrderId = orderId });
    }
}

public class CreateOrderDto
{
    public string CustomerName { get; set; }
}