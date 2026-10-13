namespace sequor_backend.Contracts.Users;

/// <summary>Registered user details.</summary>
public sealed record UserResponse(
    string Email,
    string Name,
    DateTimeOffset InitialDate,
    DateTimeOffset? EndDate);
