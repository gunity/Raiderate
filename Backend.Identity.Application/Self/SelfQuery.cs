using Backend.Identity.Application.Common.Abstractions.Messaging;

namespace Backend.Identity.Application.Self;

public sealed record SelfQuery : IQuery<SelfResult>
{
    public static SelfQuery Default { get; } = new();
}