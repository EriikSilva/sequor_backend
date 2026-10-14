namespace sequor_backend.Contracts.Users;

public sealed record UserResponse(
    string Email,
    string Name,
    DateTimeOffset InitialDate,
    DateTimeOffset? EndDate);
