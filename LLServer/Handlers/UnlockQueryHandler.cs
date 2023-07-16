using LLServer.Common;
using LLServer.Models.Responses;
using MediatR;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

public record UnlockQuery : IRequest<ResponseContainer>;

public class UnlockQueryHandler : IRequestHandler<UnlockQuery, ResponseContainer>
{
    public async Task<ResponseContainer> Handle(UnlockQuery request, CancellationToken cancellationToken)
    {
        return StaticResponses.EmptyResponse;
    }
}