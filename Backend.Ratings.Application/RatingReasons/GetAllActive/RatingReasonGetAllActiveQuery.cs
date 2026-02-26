using Backend.Ratings.Application.Common.Abstractions.Messaging;

namespace Backend.Ratings.Application.RatingReasons.GetAllActive;

public sealed record RatingReasonGetAllActiveQuery : IQuery<RatingReasonGetAllActiveResult>
{
    public static readonly RatingReasonGetAllActiveQuery Default = new();
}