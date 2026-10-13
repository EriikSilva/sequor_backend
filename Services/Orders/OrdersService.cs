using Microsoft.EntityFrameworkCore;
using sequor_backend.Contracts.Orders;
using sequor_backend.data;

namespace sequor_backend.Services.Orders;

public sealed class OrdersService(appDbContext dbContext) : IOrdersService
{
    public async Task<OrdersListResponse> GetOrdersAsync(CancellationToken cancellationToken)
    {
        var orders = await dbContext.OrderProduction
            .AsNoTracking()
            .OrderBy(order => order.Order)
            .ToListAsync(cancellationToken);

        var productCodes = orders
            .Select(order => order.ProductCode)
            .Where(productCode => !string.IsNullOrEmpty(productCode))
            .Distinct()
            .ToList();

        var products = await dbContext.Product
            .AsNoTracking()
            .Where(product => productCodes.Contains(product.ProductCode))
            .ToDictionaryAsync(product => product.ProductCode, cancellationToken);

        var materials = await (
            from productMaterial in dbContext.ProductMaterial.AsNoTracking()
            join material in dbContext.Material.AsNoTracking()
                on productMaterial.MaterialCode equals material.MaterialCode
            where productCodes.Contains(productMaterial.ProductCode)
            orderby productMaterial.ProductCode, productMaterial.SortOrder
            select new
            {
                productMaterial.ProductCode,
                Material = new MaterialResponse(
                    material.MaterialCode,
                    material.MaterialDescription)
            })
            .ToListAsync(cancellationToken);

        var materialsByProduct = materials
            .GroupBy(row => row.ProductCode)
            .ToDictionary(
                group => group.Key,
                group => (IReadOnlyList<MaterialResponse>)group
                    .Select(row => row.Material)
                    .ToArray());

        var responseOrders = orders
            .Select(order =>
            {
                products.TryGetValue(order.ProductCode, out var product);
                materialsByProduct.TryGetValue(order.ProductCode, out var productMaterials);

                return new OrderResponse(
                    order.Order,
                    order.Quantity,
                    order.ProductCode,
                    product?.ProductDescription,
                    product?.Image,
                    product?.CycleTime ?? 0m,
                    productMaterials ?? Array.Empty<MaterialResponse>());
            })
            .ToArray();

        return new OrdersListResponse(responseOrders);
    }
}
