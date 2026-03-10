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
        var votes = await voteRepository.GetAllByPlayerIdReadonlyAsync(
            request.PlayerId,
            request.Limit,
            cancellationToken);

        var votesWithComments = votes
            .Where(x => !string.IsNullOrWhiteSpace(x.Comment))
            .ToList();

        var logins = await identityClient.GetLoginAsync(
            votesWithComments.Select(x => x.FromUserId).Distinct().ToArray(),
            cancellationToken);

        var loginsByUserId = logins.Items.ToDictionary(x => x.Id, x => x.Login);

        var comments = votesWithComments
            .Select(x =>
            {
                var login = loginsByUserId.GetValueOrDefault(x.FromUserId.ToString(), "unknown");
                return new VoteComment(x.Id.ToString(), login, x.Comment!, x.Reason.Value, x.CreatedAt);
            })
            .ToArray();

        return new GetCommentsResult(comments);
    }
}