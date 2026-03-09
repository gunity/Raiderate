using Backend.Identity.Application.Common.Abstractions.Messaging;

namespace Backend.Identity.Application.GetLogin;

public sealed record GetLoginsByUserIdQuery(long[] Ids) : IQuery<GetLoginsByUserIdResult>;