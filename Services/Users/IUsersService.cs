#nullable enable

using sequor_backend.Contracts.Users;

namespace sequor_backend.Services.Users;

public interface IUsersService
{
    Task<UserResponse?> CreateAsync(
        CreateUserRequest request);

    Task<UserResponse?> GetByEmailAsync(
        string email);
}
