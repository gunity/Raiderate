using Backend.Players.Application.Common.Abstractions.Persistence;
using Backend.Players.Domain.Players;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Backend.Players.Application.GetOrCreate;

public class GetOrCreatePlayerHandler(
    IPlayerRepository playerRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<GetOrCreatePlayerCommand, GetOrCreatePlayerResult>
{
    public async Task<GetOrCreatePlayerResult> Handle(GetOrCreatePlayerCommand request, CancellationToken ct)
    {
        var player = await playerRepository.GetReadonlyByNicknameAsync(request.Nickname, ct);
        if (player is not null)
        {
            return new GetOrCreatePlayerResult(player.Id, player.Nickname);
        }

        player = new Player(request.Nickname);
        
        try
        {
            await playerRepository.AddAsync(player, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return new GetOrCreatePlayerResult(player.Id, player.Nickname);
        }
        catch (DbUpdateException exception) 
            when (exception.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            player = await playerRepository.GetReadonlyByNicknameAsync(request.Nickname, ct);
            if (player is null)
            {
                throw;
            }

            return new GetOrCreatePlayerResult(player.Id, player.Nickname);
        }
    }
}