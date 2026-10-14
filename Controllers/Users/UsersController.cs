using Microsoft.AspNetCore.Mvc;
using sequor_backend.Contracts.Users;
using sequor_backend.Services.Users;

namespace sequor_backend.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserResponse>> Create(
        CreateUserRequest request)
    {
        var response = await usersService.CreateAsync(request);
        if (response is null)
        {
            return Conflict(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Email already registered",
                Detail = "A user with this email address already exists."
            });
        }

        return CreatedAtAction(
            nameof(GetByEmail),
            new { email = response.Email },
            response);
    }

    [HttpGet("{email}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> GetByEmail(
        string email)
    {
        var response = await usersService.GetByEmailAsync(email);
        return response is null ? NotFound() : Ok(response);
    }
}