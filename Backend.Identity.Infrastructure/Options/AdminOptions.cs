using System.ComponentModel.DataAnnotations;

namespace Backend.Identity.Infrastructure.Options;

public class AdminOptions
{
    [Required] public string Login { get; init; } = null!;
    [Required] public string Password { get; init; } = null!;
}