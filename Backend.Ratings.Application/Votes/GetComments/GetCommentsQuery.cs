using Backend.Ratings.Application.Common.Abstractions.Messaging;

namespace Backend.Ratings.Application.Votes.GetComments;

public sealed record GetCommentsQuery(Guid PlayerId, int Limit) : IQuery<GetCommentsResult>;
