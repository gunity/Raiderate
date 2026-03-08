using Backend.Players.Application.GetLeaderboard;
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

    [HttpGet("leaderboard")]
    public async Task<ActionResult<GetLeaderboardResult>> GetLeaderboardAsync(
        [FromQuery] LeaderboardType type = LeaderboardType.Top,
        [FromQuery] int limit = 5,
        CancellationToken ct = default)
    {
        limit = Math.Clamp(limit, 1, 50);

        var result = await mediator.Send(new GetLeaderboardQuery(type, limit), ct);
        return Ok(result);
    }
}