using System.Text.Json;
using LLServer.Common;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

public record InitializeUserDataCommand(JsonElement? Param) : IRequest<ResponseContainer>;

public class InitializeUserDataCommandHandler : IRequestHandler<InitializeUserDataCommand, ResponseContainer>
{
    private readonly ILogger<InitializeUserDataCommandHandler> logger;

    public InitializeUserDataCommandHandler(ILogger<InitializeUserDataCommandHandler> logger)
    {
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(InitializeUserDataCommand request, CancellationToken cancellationToken)
    {
        if (request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //deserialize from param
        var paramJson = request.Param.Value.GetRawText();

        var initializeUserData = JsonSerializer.Deserialize<InitializeUserData>(paramJson);

        if (initializeUserData is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        var userDataContainer = UserDataContainer.GetDummyUserDataContainer();
        userDataContainer.InitializeUserData(initializeUserData);

        var response = new ResponseContainer
        {
            Result = 200,
            Response = userDataContainer.GetUserData()
        };
        return response;
    }
}