using Backend.Ratings.Application.Common.Abstractions.Persistence;
using MediatR;

namespace Backend.Ratings.Application.Votes.GetComments;

public class GetCommentsHandler(IVoteRepository voteRepository) : IRequestHandler<GetCommentsQuery, GetCommentsResult>
{
    public async Task<GetCommentsResult> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var votes = await voteRepository.GetAllByPlayerIdReadonlyAsync(request.PlayerId, request.Limit, cancellationToken);

        var comments = votes
            .Select(x => x.Comment?.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => new VoteComment(x!))
            .ToList();
        
        return new GetCommentsResult(comments);
    }
}