using System.ComponentModel.DataAnnotations;

namespace Backend.Ratings.Infrastructure.Options;

public class DbOptions
{
    [Required] public string Connection { get; init; } = null!;
}