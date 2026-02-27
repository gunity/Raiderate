using System.ComponentModel.DataAnnotations;

namespace Backend.Ratings.Infrastructure.Options;

public class DestinationOptions
{
    [Required] public string PlayersGrpcUrl { get; init; } = null!;
}