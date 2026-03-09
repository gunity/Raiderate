using Backend.Ratings.Application.Common.Abstractions.Messaging;

namespace Backend.Ratings.Application.RatingReasons.Update;

public sealed record RatingReasonUpdateBody(
    string Code,
    int Value,
    bool IsActive
) : ICommand;