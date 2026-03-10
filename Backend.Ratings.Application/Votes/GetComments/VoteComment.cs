namespace Backend.Ratings.Application.Votes.GetComments;

public sealed record VoteComment(string Id, string UserLogin, string Text, int Delta, DateTime CreatedAt);