using Backend.Ratings.Domain.RatingReasons;

namespace Backend.Ratings.Domain.Votes;

public class Vote
{
    public long Id { get; private set; }
    public long PlayerId { get; private set; }
    public long FromUserId  { get; private set; }
    public long ReasonId { get; private set; }
    public RatingReason Reason { get; private set; } = null!;
    public string? Comment { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Vote() { } // EF

    public Vote(long playerId, long fromUserId, long reasonId, string? comment = null)
    {
        PlayerId = playerId;
        FromUserId = fromUserId;
        ReasonId = reasonId;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
    }
}