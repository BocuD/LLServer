using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record GameExitCommand(RequestBase request) : IRequest<ResponseContainer>;

public class GameExitCommandHandler : IRequestHandler<GameExitCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<GameExitCommandHandler> logger;
    private readonly SessionHandler sessionHandler;

    public GameExitCommandHandler(ApplicationDbContext dbContext, ILogger<GameExitCommandHandler> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }

    public async Task<ResponseContainer> Handle(GameExitCommand command, CancellationToken cancellationToken)
    {
        if (command.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        GameSession? session = await sessionHandler.GetSession(command.request, cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        string paramJson = command.request.Param.Value.GetRawText();

        //get game result
        GameExit? gameResult = JsonSerializer.Deserialize<GameExit>(paramJson);
        if (gameResult is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session);
        
        //update flags
        container.Flags = gameResult.Flags;
        
        //remove session
        dbContext.Sessions.Remove(session);
        
        //write to database
        await container.SaveChanges(cancellationToken);
        
        return new ResponseContainer
        {
            Result = 200,
            Response = new ResponseBase()
        };
    }
}