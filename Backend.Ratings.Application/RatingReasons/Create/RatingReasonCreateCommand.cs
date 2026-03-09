using Backend.Ratings.Application.Common.Abstractions.Messaging;

namespace Backend.Ratings.Application.RatingReasons.Create;

public sealed record RatingReasonCreateCommand(
    string Code,
    int Value
) : ICommand<RatingReasonCreateResult>;