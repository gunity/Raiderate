using System.ComponentModel.DataAnnotations;

namespace Backend.Shared.Options;

public class RabbitMqOptions
{
    [Required] public string Host { get; init; } = null!;
    [Required] public string User { get; init; } = null!;
    [Required] public string Password { get; init; } = null!;
}