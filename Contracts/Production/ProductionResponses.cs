#nullable enable

namespace sequor_backend.Contracts.Production;

/// <summary>Production records associated with a user.</summary>
public sealed record ProductionListResponse(IReadOnlyList<ProductionResponse> Productions);

/// <summary>Production appointment details.</summary>
public sealed record ProductionResponse(
    string Email,
    string Order,
    string MaterialCode,
    DateTimeOffset StartDateAppointment,
    DateTimeOffset? EndDateAppointment,
    decimal Quantity,
    decimal CycleTime);

/// <summary>Result of recording production, including any informational message.</summary>
public sealed record ProductionCreatedResponse(
    ProductionResponse Production,
    string? Message);
