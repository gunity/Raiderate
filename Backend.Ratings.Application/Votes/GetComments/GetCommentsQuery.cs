using Backend.Ratings.Application.Common.Abstractions.Messaging;

namespace Backend.Ratings.Application.Votes.GetComments;

public sealed record GetCommentsQuery(long PlayerId, int Limit) : IQuery<GetCommentsResult>;
