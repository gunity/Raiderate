using Backend.Identity.Application.Common.Abstractions.Messaging;

namespace Backend.Identity.Application.GetLogin;

public sealed record GetLoginQuery(long Id) : IQuery<GetLoginResult>;