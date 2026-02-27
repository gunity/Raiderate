using Backend.Ratings.Application.Votes.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Ratings.Api.Controllers.Votes;

[ApiController]
[Route("api/votes")]
public class Controller(IMediator mediator) : ControllerBase
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
}