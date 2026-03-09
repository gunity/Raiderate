using Backend.Ratings.Application.Common.Abstractions;
using Backend.Ratings.Application.Common.Abstractions.Persistence;
using MediatR;

namespace Backend.Ratings.Application.Votes.GetComments;

public class GetCommentsHandler(
    IVoteRepository voteRepository,
    IIdentityClient identityClient
) : IRequestHandler<GetCommentsQuery, GetCommentsResult>
{
    public async Task<GetCommentsResult> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var votes = await voteRepository.GetAllByPlayerIdReadonlyAsync(request.PlayerId, request.Limit, cancellationToken);

        var commentTasks = votes
            .Where(x => !string.IsNullOrWhiteSpace(x.Comment))
            .Select(async x =>
            {
                var login = await identityClient.GetLoginAsync(x.FromUserId, cancellationToken);
                return new VoteComment(login.Login, x.Comment!, x.Reason.Value, x.CreatedAt);
            })
            .ToList();
        
        var comments = await Task.WhenAll(commentTasks);
        
        return new GetCommentsResult(comments);
    }
}