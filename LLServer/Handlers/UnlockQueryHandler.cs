using LLServer.Common;
using LLServer.Models;
using MediatR;

namespace LLServer.Handlers;

public record UnlockQuery(): IRequest<ResponseContainer>;

public class UnlockQueryHandler : IRequestHandler<UnlockQuery, ResponseContainer>
{
    public async Task<ResponseContainer> Handle(UnlockQuery request, CancellationToken cancellationToken)
    {
        return StaticResponses.EmptyResponse;
    }
}