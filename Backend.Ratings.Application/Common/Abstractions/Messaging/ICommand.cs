using MediatR;

namespace Backend.Ratings.Application.Common.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;
public interface ICommand : IRequest<Unit>;