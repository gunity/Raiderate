using Backend.Ratings.Application.Common.Abstractions.Messaging;

namespace Backend.Ratings.Application.Votes.Create;

public sealed record VotesCreateCommand(
    string Nickname,
    Guid ReasonId,
    string Comment
) : ICommand<VotesCreateResult>;