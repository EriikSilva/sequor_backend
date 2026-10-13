using Microsoft.AspNetCore.Mvc;
using sequor_backend.Contracts.Orders;
using sequor_backend.Services.Orders;

namespace sequor_backend.Controllers.Orders;

[ApiController]
[Route("api/[controller]")]
public sealed class OrdersController(IOrdersService ordersService) : ControllerBase
{
    /// <summary>Returns all orders with their product and material details.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(OrdersListResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<OrdersListResponse>> GetOrders(
        CancellationToken cancellationToken)
    {
        var response = await ordersService.GetOrdersAsync(cancellationToken);
        return Ok(response);
    }
}