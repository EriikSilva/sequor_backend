using System.ComponentModel.DataAnnotations;

namespace sequor_backend.Contracts.Production;

public sealed record CreateProductionRequest
{
    [Required, EmailAddress, MaxLength(100)]
    public required string Email { get; init; }

    [Required, MaxLength(50)]
    public required string Order { get; init; }

    [Required, MaxLength(50)]
    public required string MaterialCode { get; init; }

    [Required]
    public DateTimeOffset? StartDateAppointment { get; init; }

    [Required]
    public DateTimeOffset? EndDateAppointment { get; init; }

    [Range(
        typeof(decimal),
        "0.01",
        "9999999999999999.99",
        ParseLimitsInInvariantCulture = true)]
    public decimal Quantity { get; init; }

    [Range(
        typeof(decimal),
        "0.01",
        "9999999999999999.99",
        ParseLimitsInInvariantCulture = true)]
    public decimal CycleTime { get; init; }

    [Range(
        typeof(decimal),
        "0.01",
        "9999999999999999.99",
        ParseLimitsInInvariantCulture = true)]
    public decimal CycleTimeElapsed { get; init; }
}
