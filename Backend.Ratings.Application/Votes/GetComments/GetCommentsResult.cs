namespace Backend.Ratings.Application.Votes.GetComments;

public sealed record GetCommentsResult(IReadOnlyList<VoteComment> Items);