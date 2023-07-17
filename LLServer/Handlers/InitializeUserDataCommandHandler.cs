using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

public record InitializeUserDataCommand(RequestBase request) : IRequest<ResponseContainer>;

public class InitializeUserDataCommandHandler : IRequestHandler<InitializeUserDataCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<InitializeUserDataCommandHandler> logger;

    public InitializeUserDataCommandHandler(ApplicationDbContext dbContext, ILogger<InitializeUserDataCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(InitializeUserDataCommand command, CancellationToken cancellationToken)
    {
        if (command.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //deserialize from param
        var paramJson = command.request.Param.Value.GetRawText();

        //get user data from db
        Session? session = await dbContext.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => 
                s.SessionId == command.request.SessionKey, 
            cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        var initializeUserData = JsonSerializer.Deserialize<InitializeUserData>(paramJson);

        if (initializeUserData is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        var userDataContainer = UserDataContainer.GetDummyUserDataContainer();
        userDataContainer.InitializeUserData(initializeUserData);
        
        session.User.Name = initializeUserData.UserData.Name;
        session.User.Initialized = true;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var response = new ResponseContainer
        {
            Result = 200,
            Response = userDataContainer.GetUserData()
        };
        return response;
    }
}