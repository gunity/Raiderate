using System.Text.Json.Serialization;
using Backend.Ratings.Domain.RatingReasons;

namespace Backend.Ratings.Application.RatingReasons.GetAllActive;

public sealed record RatingReasonGetAllActiveResult(
    IEnumerable<RatingReason> Reasons
);