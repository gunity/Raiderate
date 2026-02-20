using System.ComponentModel.DataAnnotations;

namespace Backend.Shared.Options;

public class JwtOptions
{
    [Required] public string CookieName { get; set; } = null!;
    [Required] public string Issuer { get; set; } = null!;
    [Required] public string Audience { get; init; } = null!;
    [Required] public string Key { get; init; } = null!;
    [Required] public int ExpireMinutes { get; set; }
}