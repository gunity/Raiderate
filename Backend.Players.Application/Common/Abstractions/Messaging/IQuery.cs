using MediatR;

namespace Backend.Players.Application.Common.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;