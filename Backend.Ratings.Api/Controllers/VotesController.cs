using Backend.Ratings.Application.Votes.Create;
using Backend.Ratings.Application.Votes.GetComments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Ratings.Api.Controllers;

[ApiController]
[Route("api/votes")]
public class VotesController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VotesCreateResult>> CreateAsync(
        [FromBody] VotesCreateCommand command,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(command, ct);
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<GetCommentsResult>> GetCommentsAsync(
        [FromQuery] long playerId,
        [FromQuery] int limit = 5,
        CancellationToken ct = default)
    {
        limit = Math.Clamp(limit, 1, 50);
        
        var result = await mediator.Send(new GetCommentsQuery(playerId, limit), ct);
        
        return Ok(result);
    }
}