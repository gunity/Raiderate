using System.ComponentModel.DataAnnotations;

namespace Backend.Shared.Options;

public class JwtOptions
{
    [Required] public string AccessTokenName { get; set; } = null!;
    [Required] public string RefreshTokenName { get; set; } = null!;
    [Required] public string Issuer { get; set; } = null!;
    [Required] public string Audience { get; init; } = null!;
    [Required] public string Key { get; init; } = null!;
    [Required] public int AccessTokenExpireMinutes { get; set; }
    [Required] public int RefreshTokenExpireDays { get; set; }
}