using Backend.Contracts.Events;
using Backend.Players.Application.ApplyVote;
using MassTransit;
using MediatR;

namespace Backend.Players.Infrastructure.Consumers;

public class VoteCreatedConsumer(IMediator mediator) : IConsumer<VoteCreated>
{
    public async Task Consume(ConsumeContext<VoteCreated> context)
    {
        var message = context.Message;
        var command = new ApplyVoteCommand(message.VoteId, message.PlayerId, message.Delta);
        await mediator.Send(command, context.CancellationToken);
    }
}