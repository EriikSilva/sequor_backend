using System.ComponentModel.DataAnnotations;

namespace sequor_backend.Contracts.Production;

/// <summary>Payload for recording production against an order.</summary>
public sealed record CreateProductionRequest
{
    /// <summary>Email address of the user recording production.</summary>
    [Required, EmailAddress, MaxLength(100)]
    public required string Email { get; init; }

    /// <summary>Existing order identifier.</summary>
    [Required, MaxLength(50)]
    public required string Order { get; init; }

    /// <summary>Material used for this order's product.</summary>
    [Required, MaxLength(50)]
    public required string MaterialCode { get; init; }

    /// <summary>Start of the production appointment.</summary>
    [Required]
    public DateTimeOffset? StartDateAppointment { get; init; }

    /// <summary>End of the production appointment.</summary>
    [Required]
    public DateTimeOffset? EndDateAppointment { get; init; }

    /// <summary>Quantity produced in this appointment.</summary>
    [Range(
        typeof(decimal),
        "0.01",
        "9999999999999999.99",
        ParseLimitsInInvariantCulture = true)]
    public decimal Quantity { get; init; }

    /// <summary>Cycle time recorded for this appointment.</summary>
    [Range(
        typeof(decimal),
        "0.01",
        "9999999999999999.99",
        ParseLimitsInInvariantCulture = true)]
    public decimal CycleTime { get; init; }

    /// <summary>Elapsed cycle time recorded for this appointment.</summary>
    [Range(
        typeof(decimal),
        "0.01",
        "9999999999999999.99",
        ParseLimitsInInvariantCulture = true)]
    public decimal CycleTimeElapsed { get; init; }
}
