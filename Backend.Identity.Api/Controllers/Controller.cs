using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.Identity.Api.Services;
using Backend.Identity.Application.Common.Abstractions;
using Backend.Identity.Application.Login;
using Backend.Identity.Application.Register;
using Backend.Identity.Application.Self;
using Backend.Shared.Exceptions;
using Backend.Shared.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        
        var accessToken = tokenService.GetAccessToken(result.Id, result.Login, result.Role);
        var refreshToken = tokenService.GetRefreshToken(result.Id, result.Login, result.Role);
        cookieService.WriteCookie(Response.Cookies, accessToken, refreshToken);
        
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> LoginAsync(
        [FromBody] LoginCommand command,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(command, ct);

        var accessToken = tokenService.GetAccessToken(result.Id, result.Login, result.Role);
        var refreshToken = tokenService.GetRefreshToken(result.Id, result.Login, result.Role);
        cookieService.WriteCookie(Response.Cookies, accessToken, refreshToken);
        
        return Ok(result);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        cookieService.DeleteCookie(Response.Cookies);
        
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAsync(
        CancellationToken  ct = default)
    {
        if (!cookieService.TryGetRefreshToken(Request.Cookies, out var token))
        {
            throw new UnauthorizedAppException("Refresh token not found");
        }

        if (!await tokenService.ValidateRefreshToken(token, ct))
        {
            throw new UnauthorizedAppException("Invalid refresh token");
        }
        
        var result = await mediator.Send(SelfQuery.Default, ct);
        
        var accessToken = tokenService.GetAccessToken(result.Id, result.Login, result.Role);
        var refreshToken = tokenService.GetRefreshToken(result.Id, result.Login, result.Role);
        cookieService.WriteCookie(Response.Cookies, accessToken, refreshToken);
        
        return Ok();
    }

    [HttpGet("self")]
    public async Task<ActionResult<SelfResult>> SelfAsync(
        CancellationToken ct = default)
    {
        var result = await mediator.Send(SelfQuery.Default, ct);
        
        return Ok(result);
    }
}