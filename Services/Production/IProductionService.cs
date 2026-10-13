#nullable enable

using sequor_backend.Contracts.Production;

namespace sequor_backend.Services.Production;

public interface IProductionService
{
    Task<IReadOnlyList<ProductionResponse>> GetByEmailAsync(
        string email);

    Task<ProductionCreateResult> CreateAsync(
        CreateProductionRequest request);
}
