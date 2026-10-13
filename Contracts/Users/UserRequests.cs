using System.ComponentModel.DataAnnotations;

namespace sequor_backend.Contracts.Users;

public sealed record CreateUserRequest
{
    [Required, EmailAddress, MaxLength(100)]
    public required string Email { get; init; }

    [Required, MaxLength(50)]
    public required string Name { get; init; }
}
