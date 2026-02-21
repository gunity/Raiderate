using Backend.Players.Application.GetPlayer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Players.Api.Controllers;

[ApiController]
[Route("api/players")]
public class Controller(
    IMediator mediator
) : ControllerBase
{
    [HttpGet("{nickname}/summary")]
    public async Task<ActionResult<GetPlayerResult>> GetPlayerAsync(
        [FromRoute] string nickname,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetPlayerQuery(nickname), ct);
        
        return Ok(result);
    }
}