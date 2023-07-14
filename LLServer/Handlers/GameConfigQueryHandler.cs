using System.Text.Json;
using LLServer.Common;
using LLServer.Models;
using MediatR;

namespace LLServer.Handlers;

public record GameConfigQuery() : IRequest<ResponseContainer>;

public class GameConfigQueryHandler : IRequestHandler<GameConfigQuery, ResponseContainer>
{
    private readonly ILogger<GameConfigQueryHandler> logger;

    public GameConfigQueryHandler(ILogger<GameConfigQueryHandler> logger)
    {
        this.logger = logger;
    }
    
    public async Task<ResponseContainer> Handle(GameConfigQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponseContainer()
        {
            Result = 200,
            Response = GameConfigResponse.DefaultGameConfigResponse()
        };
        
        logger.LogInformation("Get GameConfig response: {Response}", JsonSerializer.Serialize(response));
        
        return response;
    }
}