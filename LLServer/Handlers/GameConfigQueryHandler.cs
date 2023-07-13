using LLServer.Common;
using LLServer.Models;
using MediatR;

namespace LLServer.Handlers;

public record GameConfigQuery() : IRequest<ResponseContainer>;

public class GameConfigQueryHandler : IRequestHandler<GameConfigQuery, ResponseContainer>
{
    public async Task<ResponseContainer> Handle(GameConfigQuery request, CancellationToken cancellationToken)
    {
        return StaticResponses.EmptyResponse;
    }
}