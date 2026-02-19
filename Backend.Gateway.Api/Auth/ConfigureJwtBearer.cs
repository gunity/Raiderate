using System.Text;
using Backend.Gateway.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Gateway.Api.Auth;

internal sealed class ConfigureJwtBearer(IOptions<JwtOptions> jwtOptions) : IConfigureOptions<JwtBearerOptions>
{
    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Value.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtOptions.Value.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key)),

            ValidateLifetime = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.TryGetValue(jwtOptions.Value.CookieName, out var token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    }
}