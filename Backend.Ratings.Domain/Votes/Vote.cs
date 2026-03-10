using Backend.Ratings.Domain.RatingReasons;

namespace Backend.Ratings.Domain.Votes;

public class Vote
{
    public Guid Id { get; private set; }
    public Guid PlayerId { get; private set; }
    public Guid FromUserId  { get; private set; }
    public Guid ReasonId { get; private set; }
    public RatingReason Reason { get; private set; } = null!;
    public string? Comment { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Vote() { } // EF

    public Vote(Guid playerId, Guid fromUserId, Guid reasonId, string? comment = null)
    {
        Id = Guid.NewGuid();
        PlayerId = playerId;
        FromUserId = fromUserId;
        ReasonId = reasonId;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
    }
}