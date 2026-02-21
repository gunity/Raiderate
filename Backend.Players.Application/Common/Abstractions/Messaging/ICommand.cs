using MediatR;

namespace Backend.Players.Application.Common.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;