using MediatR;
using RamenStore.Domain.Abstractions;

namespace RamenStore.Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}