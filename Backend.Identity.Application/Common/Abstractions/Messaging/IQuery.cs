using MediatR;

namespace Backend.Identity.Application.Common.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;