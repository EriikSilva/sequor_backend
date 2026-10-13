#nullable enable

using Microsoft.EntityFrameworkCore;
using sequor_backend.Contracts.Users;
using sequor_backend.data;
using sequor_backend.models;

namespace sequor_backend.Services.Users;

public sealed class UsersService(appDbContext dbContext) : IUsersService
{
    public async Task<UserResponse?> CreateAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        if (await dbContext.Users.AnyAsync(
                user => user.Email == request.Email,
                cancellationToken))
        {
            throw new InvalidOperationException("User with the specified email address already exists.");
        }

        var user = new User
        {
            Email = request.Email,
            Name = request.Name,
            InitialDate = DateTime.UtcNow,
            EndDate = null
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ToResponse(user);
    }

    public async Task<UserResponse?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(user => user.Email == email, cancellationToken);

        return user is null ? null : ToResponse(user);
    }

    private static UserResponse ToResponse(User user) =>
        new(
            user.Email,
            user.Name,
            new DateTimeOffset(DateTime.SpecifyKind(user.InitialDate, DateTimeKind.Utc)),
            user.EndDate is null
                ? null
                : new DateTimeOffset(DateTime.SpecifyKind(user.EndDate.Value, DateTimeKind.Utc)));
}
