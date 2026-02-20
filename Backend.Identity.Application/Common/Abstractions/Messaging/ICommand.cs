using MediatR;

namespace Backend.Identity.Application.Common.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;
// public interface ICommand : IRequest<Unit>; // yagni :) 