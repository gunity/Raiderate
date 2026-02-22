using MediatR;

namespace Backend.Ratings.Application.Common.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;