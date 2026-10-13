namespace sequor_backend.Contracts.Orders;
public sealed record OrdersListResponse(IReadOnlyList<OrderResponse> Orders);

public sealed record OrderResponse(
    string Order,
    decimal Quantity,
    string ProductCode,
    string ProductDescription,
    string Image,
    decimal CycleTime,
    IReadOnlyList<MaterialResponse> Materials);

public sealed record MaterialResponse(string MaterialCode, string MaterialDescription);
