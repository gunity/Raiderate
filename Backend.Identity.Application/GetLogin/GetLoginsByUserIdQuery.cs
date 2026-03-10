using Backend.Identity.Application.Common.Abstractions.Messaging;

namespace Backend.Identity.Application.GetLogin;

public sealed record GetLoginsByUserIdQuery(Guid[] Ids) : IQuery<GetLoginsByUserIdResult>;