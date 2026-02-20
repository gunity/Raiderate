using Backend.Identity.Api.Services;
using Backend.Identity.Application.Common.Abstractions;
using Backend.Identity.Application.Login;
using Backend.Identity.Application.Register;
using Backend.Identity.Application.Self;
using Backend.Shared.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Identity.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class Controller(
    IMediator mediator,
    ICookieService cookieService,
    ITokenService tokenService
    ) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResult>> RegisterAsync(
        RegisterCommand command,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(command, ct);
        
        var token = tokenService.IssueToken(result.Id, result.Login, result.Role);
        cookieService.WriteCookie(Response.Cookies, token);
        
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> LoginAsync(
        [FromBody] LoginCommand command,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(command, ct);

        var token = tokenService.IssueToken(result.Id, result.Login, result.Role);
        cookieService.WriteCookie(Response.Cookies, token);
        
        return Ok(result);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        cookieService.DeleteCookie(Response.Cookies);
        
        return Ok();
    }

    [Authorize(Roles = nameof(AppRole.User))]
    [HttpGet("self")]
    public async Task<IActionResult> Self(
        CancellationToken ct = default)
    {
        var result = await mediator.Send(SelfQuery.Default, ct);
        
        return Ok(result);
    }
}