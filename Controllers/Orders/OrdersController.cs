using Microsoft.AspNetCore.Mvc;
using sequor_backend.Contracts.Orders;
using sequor_backend.Services.Orders;

namespace sequor_backend.Controllers.Orders;

[ApiController]
[Route("api/[controller]")]
public sealed class OrdersController(IOrdersService ordersService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(OrdersListResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<OrdersListResponse>> GetOrders()
    {
        var response = await ordersService.GetOrdersAsync();
        return Ok(response);
    }
}