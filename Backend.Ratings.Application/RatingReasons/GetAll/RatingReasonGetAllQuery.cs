using Backend.Ratings.Application.Common.Abstractions.Messaging;

namespace Backend.Ratings.Application.RatingReasons.GetAll;

public sealed record RatingReasonGetAllQuery : IQuery<RatingReasonGetAllResult>
{
    public static readonly RatingReasonGetAllQuery Default = new();
}