using System.ComponentModel.DataAnnotations;

namespace Backend.Gateway.Infrastructure.Options;

public class DestinationOptions
{
    [Required] public string IdentityUrl { get; init; } = null!;
    [Required] public string PlayersUrl { get; init; } = null!;
}