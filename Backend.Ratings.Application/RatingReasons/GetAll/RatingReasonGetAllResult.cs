using Backend.Ratings.Domain.RatingReasons;

namespace Backend.Ratings.Application.RatingReasons.GetAll;

public sealed record RatingReasonGetAllResult(
    IEnumerable<RatingReason> Reasons
);
