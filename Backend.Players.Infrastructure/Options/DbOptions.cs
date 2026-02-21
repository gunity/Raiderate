using System.ComponentModel.DataAnnotations;

namespace Backend.Players.Infrastructure.Options;

public class DbOptions
{
    [Required] public string Connection { get; init; } = null!;
}