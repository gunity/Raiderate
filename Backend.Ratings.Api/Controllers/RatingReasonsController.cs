using Backend.Ratings.Application.RatingReasons.Create;
using Backend.Ratings.Application.RatingReasons.GetAll;
using Backend.Ratings.Application.RatingReasons.GetAllActive;
using Backend.Ratings.Application.RatingReasons.Update;
using Backend.Shared.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Ratings.Api.Controllers;

[ApiController]
[Route("api/rating-reasons")]
public class RatingReasonsController(
    IMediator mediator
) : ControllerBase
{
    [HttpPost("admin")]
    [Authorize(Roles = AppRoleWire.Admin)]
    public async Task<ActionResult<RatingReasonCreateResult>> CreateAsync(
        [FromBody] RatingReasonCreateCommand command,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<RatingReasonGetAllActiveResult>> GetAllActiveAsync(
        CancellationToken ct = default)
    {
        var result = await mediator.Send(RatingReasonGetAllActiveQuery.Default, ct);
        return Ok(result);
    }

    [HttpGet("admin")]
    [Authorize(Roles = AppRoleWire.Admin)]
    public async Task<ActionResult<RatingReasonGetAllResult>> GetAllAsync(
        CancellationToken ct = default)
    {
        var result = await mediator.Send(RatingReasonGetAllQuery.Default, ct);
        return Ok(result);
    }

    [HttpPut("admin/{id:guid}")]
    [Authorize(Roles = AppRoleWire.Admin)]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] RatingReasonUpdateBody body,
        CancellationToken ct = default)
    {
        var command = new RatingReasonUpdateCommand(id, body.Code, body.Value, body.IsActive);
        await mediator.Send(command, ct);
        return NoContent();
    }
}