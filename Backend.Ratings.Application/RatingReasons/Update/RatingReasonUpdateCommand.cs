using Backend.Ratings.Application.Common.Abstractions.Messaging;
using MediatR;

namespace Backend.Ratings.Application.RatingReasons.Update;

public sealed record RatingReasonUpdateCommand(
    long Id,
    int? Value,
    bool? IsActive
) : ICommand<Unit>;