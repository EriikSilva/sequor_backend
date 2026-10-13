using sequor_backend.Contracts.Orders;

namespace sequor_backend.Services.Orders;

public interface IOrdersService
{
    Task<OrdersListResponse> GetOrdersAsync(CancellationToken cancellationToken);
}
