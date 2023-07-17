using System.Text.Json;
using LLServer.Common;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;

// ReSharper disable UnusedType.Global
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

        string paramJson = request.Param.Value.GetRawText();

        SetUserData? setUserData = JsonSerializer.Deserialize<SetUserData>(paramJson);

        if (setUserData is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        UserDataContainer userDataContainer = UserDataContainer.GetDummyUserDataContainer();
        userDataContainer.SetUserData(setUserData);

        ResponseContainer response = new ResponseContainer
        {
            Result = 200,
            Response = userDataContainer.GetUserData()
        };
        return response;
    }
}