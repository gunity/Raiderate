using System.ComponentModel.DataAnnotations;

namespace Backend.Ratings.Infrastructure.Options;

public class DestinationOptions
{
    [Required] public string PlayersGrpcUrl { get; init; } = null!;
    [Required] public string IdentityGrpcUrl { get; init; } = null!;
}