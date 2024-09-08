using MediatR;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}