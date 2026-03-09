using System.ComponentModel.DataAnnotations;

namespace Backend.Identity.Infrastructure.Options;

public class DbOptions
{
    [Required] public string Connection { get; init; } = null!;
}