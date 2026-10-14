#nullable enable

using sequor_backend.Contracts.Production;

namespace sequor_backend.Services.Production;

public enum ProductionCreateError
{
    None,
    UserNotFound,
    OrderNotFound,
    MaterialNotInOrder,
    AppointmentOutsideUserPeriod,
    EndBeforeStart,
    QuantityExceedsOrder
}

public sealed record ProductionCreateResult(
    ProductionCreateError Error,
    ProductionCreatedResponse? Response);
