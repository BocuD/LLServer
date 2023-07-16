using System.Text.Json;
using LLServer.Models;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;

namespace LLServer.Handlers;

public record GetGameEntryQuery() : IRequest<ResponseContainer>;

public class GetGameEntryQueryHandler : IRequestHandler<GetGameEntryQuery, ResponseContainer>
{
    private readonly ILogger<GetGameEntryQueryHandler> logger;

    public GetGameEntryQueryHandler(ILogger<GetGameEntryQueryHandler> logger)
    {
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(GetGameEntryQuery request, CancellationToken cancellationToken)
    {
        var userDataContainer = UserDataContainer.GetDummyUserDataContainer();
        var response = new ResponseContainer()
        {
            Result = 200,
            Response = userDataContainer.GetGameEntry()
        };

        //log response json
        logger.LogInformation("GameEntry response: {Response}", JsonSerializer.Serialize(response));
        return response;
    }
}