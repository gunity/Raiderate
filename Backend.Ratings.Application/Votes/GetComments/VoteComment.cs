namespace Backend.Ratings.Application.Votes.GetComments;

public sealed record VoteComment(string UserLogin, string Text, int Delta, DateTime CreatedAt);