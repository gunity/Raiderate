namespace Backend.Contracts.Events;

public sealed record VoteCreated(
    long PlayerId,
    int Delta,
    long FromUserId,
    long ReasonId
);