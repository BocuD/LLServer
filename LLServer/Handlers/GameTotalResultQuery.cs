using LLServer.Common;
using LLServer.Models.Responses;
using MediatR;

namespace LLServer.Handlers;

public record GameTotalResultQuery : IRequest<ResponseContainer>;

public class GameTotalResultQueryHandler : IRequestHandler<GameTotalResultQuery, ResponseContainer>
{
    public async Task<ResponseContainer> Handle(GameTotalResultQuery request, CancellationToken cancellationToken)
    {
        return StaticResponses.EmptyResponse;
    }
}