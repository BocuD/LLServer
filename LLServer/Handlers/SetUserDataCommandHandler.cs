using System.Text.Json;
using LLServer.Common;
using LLServer.Models;
using LLServer.Models.UserDataModel;
using MediatR;

namespace LLServer.Handlers;

public record SetUserDataCommand(JsonElement? Param) : IRequest<ResponseContainer>;

public class SetUserDataCommandHandler : IRequestHandler<SetUserDataCommand, ResponseContainer>
{
    private readonly ILogger<SetUserDataCommandHandler> logger;

    public SetUserDataCommandHandler(ILogger<SetUserDataCommandHandler> logger)
    {
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(SetUserDataCommand request, CancellationToken cancellationToken)
    {
        if (request.Param is null) 
        {
            return StaticResponses.BadRequestResponse;
        }
                
        var paramJson = request.Param.Value.GetRawText();
        logger.LogInformation("ParamJson {ParamJson}", paramJson);
        
        var setUserData = JsonSerializer.Deserialize<SetUserData>(paramJson);

        if (setUserData is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        var userDataContainer = UserDataContainer.GetDummyUserDataContainer();
        userDataContainer.SetUserData(setUserData);
                
        //log setuserdata json and userdatacontainer json
        logger.LogInformation("SetUserData {SetUserData}", JsonSerializer.Serialize(setUserData));
        logger.LogInformation("UserDataContainer {UserDataContainer}", JsonSerializer.Serialize(userDataContainer));

        var response = new ResponseContainer
        {
            Result = 200,
            Response = userDataContainer.GetUserData()
        };
        return response;
    }
}