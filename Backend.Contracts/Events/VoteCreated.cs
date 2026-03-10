namespace Backend.Contracts.Events;

public sealed record VoteCreated(
    Guid VoteId,
    Guid PlayerId,
    int Delta,
    Guid FromUserId,
    Guid ReasonId
);