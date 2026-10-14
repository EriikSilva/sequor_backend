using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using sequor_backend.Contracts.Production;
using sequor_backend.Services.Production;

namespace sequor_backend.Controllers.Production;

[ApiController]
[Route("api/orders")]
public sealed class ProductionController(IProductionService productionService) : ControllerBase
{
    [HttpGet("GetProduction")]
    [ProducesResponseType(typeof(ProductionListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductionListResponse>> GetProduction(
        [FromQuery, Required, EmailAddress] string email)
    {
        var records = await productionService.GetByEmailAsync(email);
        return Ok(new ProductionListResponse(records));
    }

    [HttpPost("Production")]
    [ProducesResponseType(typeof(ProductionCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProductionCreatedResponse>> Create(
        CreateProductionRequest request)
    {
        var result = await productionService.CreateAsync(request);
        return result.Error switch
        {
            ProductionCreateError.None => CreatedAtAction(
                nameof(GetProduction),
                new { email = request.Email },
                result.Response),
            ProductionCreateError.UserNotFound => NotFound(Problem(
                StatusCodes.Status404NotFound,
                "User not found",
                "The email address is not registered.")),
            ProductionCreateError.OrderNotFound => NotFound(Problem(
                StatusCodes.Status404NotFound,
                "Order not found",
                "The order or its product was not found.")),
            ProductionCreateError.MaterialNotInOrder => BadRequest(Problem(
                StatusCodes.Status400BadRequest,
                "Material not associated with order",
                "The material must be associated with the product for the specified order.")),
            ProductionCreateError.AppointmentOutsideUserPeriod => BadRequest(Problem(
                StatusCodes.Status400BadRequest,
                "Appointment outside user period",
                "The appointment dates must be within the user's registered start and end dates.")),
            ProductionCreateError.EndBeforeStart => BadRequest(Problem(
                StatusCodes.Status400BadRequest,
                "Invalid appointment dates",
                "The appointment end date must be on or after its start date.")),
            ProductionCreateError.QuantityExceedsOrder => Conflict(Problem(
                StatusCodes.Status409Conflict,
                "Order quantity exceeded",
                "The total quantity reported for this order cannot exceed its ordered quantity.")),
            _ => throw new InvalidOperationException("Unknown production creation result.")
        };
    }

    private ProblemDetails Problem(int status, string title, string detail) =>
        new()
        {
            Status = status,
            Title = title,
            Detail = detail,
            Instance = Request.Path
        };
}