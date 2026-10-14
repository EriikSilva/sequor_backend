namespace sequor_backend.Contracts.Production;

public sealed record ProductionListResponse(IReadOnlyList<ProductionResponse> Productions);

public sealed record ProductionResponse(
    string Email,
    string Order,
    string MaterialCode,
    string MaterialDescription,
    DateTimeOffset StartDateAppointment,
    DateTimeOffset? EndDateAppointment,
    decimal Quantity,
    decimal CycleTime,
    decimal CycleTimeElapsed);

public sealed record ProductionCreatedResponse(
    ProductionResponse Production,
    string Message);
