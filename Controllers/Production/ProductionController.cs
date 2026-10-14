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
    [ProducesResponseType(typeof(ProductionSubmissionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProductionSubmissionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProductionSubmissionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProductionSubmissionResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProductionSubmissionResponse>> Create(
        CreateProductionRequest request)
    {
        var result = await productionService.CreateAsync(request);
        return result.Error switch
        {
            ProductionCreateError.None => CreatedAtAction(
                nameof(GetProduction),
                new { email = request.Email },
                result.Response),
            ProductionCreateError.UserNotFound => SubmissionError(
                StatusCodes.Status404NotFound,
                "O endereço de e-mail não está cadastrado."),
            ProductionCreateError.OrderNotFound => SubmissionError(
                StatusCodes.Status404NotFound,
                "A ordem ou o produto correspondente não foi encontrado."),
            ProductionCreateError.MaterialNotInOrder => SubmissionError(
                StatusCodes.Status400BadRequest,
                "O material deve estar associado ao produto da ordem informada."),
            ProductionCreateError.AppointmentOutsideUserPeriod => SubmissionError(
                StatusCodes.Status400BadRequest,
                "As datas do apontamento devem estar dentro do período cadastrado para o usuário."),
            ProductionCreateError.EndBeforeStart => SubmissionError(
                StatusCodes.Status400BadRequest,
                "A data final do apontamento deve ser igual ou posterior à data inicial."),
            ProductionCreateError.QuantityExceedsOrder => SubmissionError(
                StatusCodes.Status409Conflict,
                "A quantidade total apontada para esta ordem não pode exceder a quantidade solicitada."),
            ProductionCreateError.CycleTimeElapsedExceedsCycleTime => SubmissionError(
                StatusCodes.Status400BadRequest,
                "O tempo de ciclo decorrido deve ser maior que zero."),
            _ => throw new InvalidOperationException("Unknown production creation result.")
        };
    }

    private ActionResult<ProductionSubmissionResponse> SubmissionError(
        int httpStatus,
        string description) =>
        StatusCode(httpStatus, new ProductionSubmissionResponse(
            201,
            "E",
            description,
            null));
}