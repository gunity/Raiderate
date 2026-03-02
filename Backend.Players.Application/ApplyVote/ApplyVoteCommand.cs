using Backend.Players.Application.Common.Abstractions.Messaging;

namespace Backend.Players.Application.ApplyVote;

public sealed record ApplyVoteCommand(
    long PlayerId,
    int Delta
    //long FromUserId,
    //long ReasonId
) : ICommand;
