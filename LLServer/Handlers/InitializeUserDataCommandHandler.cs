using System.Text.Json;
using LLServer.Common;
using LLServer.Models;
using LLServer.Models.UserDataModel;
using MediatR;

namespace LLServer.Handlers;

public record InitializeUserDataCommand(string Param) : IRequest<ResponseContainer>;

public class InitializeUserDataCommandHandler : IRequestHandler<InitializeUserDataCommand, ResponseContainer>
{
    private readonly ILogger<InitializeUserDataCommandHandler> logger;

    public InitializeUserDataCommandHandler(ILogger<InitializeUserDataCommandHandler> logger)
    {
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(InitializeUserDataCommand request, CancellationToken cancellationToken)
    {
        var initializeUserData = JsonSerializer.Deserialize<InitializeUserData>(request.Param);

        if (initializeUserData is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        var userDataContainer = UserDataContainer.GetDummyUserDataContainer();
        userDataContainer.InitializeUserData(initializeUserData);
                
        logger.LogInformation("InitializeUserData {InitializeUserData}", JsonSerializer.Serialize(initializeUserData));
        logger.LogInformation("UserDataContainer {UserDataContainer}", JsonSerializer.Serialize(userDataContainer));
                
        var response = new ResponseContainer
        {
            Result = 200,
            Response = userDataContainer.GetUserData()
        };
        return response;
    }
}