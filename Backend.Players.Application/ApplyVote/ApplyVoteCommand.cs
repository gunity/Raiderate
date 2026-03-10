using Backend.Players.Application.Common.Abstractions.Messaging;

namespace Backend.Players.Application.ApplyVote;

public sealed record ApplyVoteCommand(
    Guid VoteId,
    Guid PlayerId,
    int Delta
) : ICommand;
