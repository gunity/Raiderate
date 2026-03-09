using Microsoft.AspNetCore.Authentication;

namespace Backend.Shared.Auth;

internal class AuthenticationOptions : AuthenticationSchemeOptions
{
    public const string Scheme = "Raiderate";
}