#nullable enable

using System.Data;
using Microsoft.EntityFrameworkCore;
using sequor_backend.Contracts.Production;
using sequor_backend.data;
using ProductionEntity = sequor_backend.models.Production;

namespace sequor_backend.Services.Production;

public sealed class ProductionService(appDbContext dbContext) : IProductionService
{
    public async Task<IReadOnlyList<ProductionResponse>> GetByEmailAsync(
        string email)
    {
        var records = await (
            from production in dbContext.Production.AsNoTracking()
            join material in dbContext.Material.AsNoTracking()
                on production.MaterialCode equals material.MaterialCode into productionMaterials
            from material in productionMaterials.DefaultIfEmpty()
            where production.Email == email
            orderby production.StartDateAppointment, production.Order
            select new
            {
                Production = production,
                MaterialDescription = material == null ? null : material.MaterialDescription
            })
            .ToListAsync();

        return records
            .Select(record => ToResponse(record.Production, record.MaterialDescription))
            .ToArray();
    }

    public async Task<ProductionCreateResult> CreateAsync(
        CreateProductionRequest request)
    {
        var start = request.StartDateAppointment!.Value.UtcDateTime;
        var end = request.EndDateAppointment!.Value.UtcDateTime;

        if (request.CycleTimeElapsed <= 0)
        {
            return Failure(ProductionCreateError.CycleTimeElapsedExceedsCycleTime);
        }

        if (end < start)
        {
            return Failure(ProductionCreateError.EndBeforeStart);
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync(
            IsolationLevel.Serializable);

        var user = await dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(
                candidate => candidate.Email == request.Email);
        if (user is null)
        {
            return Failure(ProductionCreateError.UserNotFound);
        }

        var userStart = DateTime.SpecifyKind(user.InitialDate, DateTimeKind.Utc);
        var userEnd = user.EndDate is null
            ? (DateTime?)null
            : DateTime.SpecifyKind(user.EndDate.Value, DateTimeKind.Utc);

        if (start < userStart || (userEnd.HasValue && end > userEnd.Value))
        {
            return Failure(ProductionCreateError.AppointmentOutsideUserPeriod);
        }

        var order = await dbContext.OrderProduction
            .AsNoTracking()
            .SingleOrDefaultAsync(
                candidate => candidate.Order == request.Order);
        if (order is null)
        {
            return Failure(ProductionCreateError.OrderNotFound);
        }

        var product = await dbContext.Product
            .AsNoTracking()
            .SingleOrDefaultAsync(
                candidate => candidate.ProductCode == order.ProductCode);
        if (product is null)
        {
            return Failure(ProductionCreateError.OrderNotFound);
        }

        var materialBelongsToOrder = await dbContext.ProductMaterial
            .AsNoTracking()
            .AnyAsync(
                productMaterial =>
                    productMaterial.ProductCode == order.ProductCode &&
                    productMaterial.MaterialCode == request.MaterialCode);
        if (!materialBelongsToOrder)
        {
            return Failure(ProductionCreateError.MaterialNotInOrder);
        }

        var materialDescription = await dbContext.Material
            .AsNoTracking()
            .Where(material => material.MaterialCode == request.MaterialCode)
            .Select(material => material.MaterialDescription)
            .SingleOrDefaultAsync();

        var quantityAlreadyRecorded = await dbContext.Production
            .Where(production => production.Order == request.Order)
            .SumAsync(production => (decimal?)production.Quantity) ?? 0m;

        if (quantityAlreadyRecorded + request.Quantity > order.Quantity)
        {
            return Failure(ProductionCreateError.QuantityExceedsOrder);
        }

        var nextId = (await dbContext.Production
            .MaxAsync(production => (long?)production.Id) ?? 0L) + 1;

        var productionRecord = new ProductionEntity
        {
            Id = nextId,
            Email = request.Email,
            Order = request.Order,
            MaterialCode = request.MaterialCode,
            StartDateAppointment = start,
            EndDateAppointment = end,
            Quantity = request.Quantity,
            CycleTime = request.CycleTime,
            CycleTimeElapsed = request.CycleTimeElapsed
        };

        dbContext.Production.Add(productionRecord);
        await dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        var message = request.CycleTime < product.CycleTime
            ? $"O tempo informado ({request.CycleTime}) é menor que o ciclo padrão da ordem ({product.CycleTime})."
            : null;

        return new ProductionCreateResult(
            ProductionCreateError.None,
            new ProductionSubmissionResponse(
                200,
                "S",
                message is null
                    ? "Produção cadastrada com sucesso."
                    : $"Produção cadastrada com sucesso. {message}",
                ToResponse(productionRecord, materialDescription),
                message));
    }

    private static ProductionCreateResult Failure(ProductionCreateError error) =>
        new(error, null);

    private static ProductionResponse ToResponse(
        ProductionEntity production,
        string? materialDescription) =>
        new(
            production.Email,
            production.Order,
            production.MaterialCode,
            materialDescription,
            new DateTimeOffset(DateTime.SpecifyKind(
                production.StartDateAppointment,
                DateTimeKind.Utc)),
            production.EndDateAppointment is null
                ? null
                : new DateTimeOffset(DateTime.SpecifyKind(
                    production.EndDateAppointment.Value,
                    DateTimeKind.Utc)),
            production.Quantity,
            production.CycleTime,
            production.CycleTimeElapsed);
}
