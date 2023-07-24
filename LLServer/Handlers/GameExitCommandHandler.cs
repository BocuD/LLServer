using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record GameExitCommand(RequestBase request) : IRequest<ResponseContainer>;

public class GameExitCommandHandler : IRequestHandler<GameExitCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<GameExitCommandHandler> logger;

    public GameExitCommandHandler(ApplicationDbContext dbContext, ILogger<GameExitCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(GameExitCommand command, CancellationToken cancellationToken)
    {
        if (command.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get session (we only need User here since we're not updating anything else right now)
        var session = await dbContext.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => 
                    s.SessionId == command.request.SessionKey, 
                cancellationToken);
        
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
        PersistentUserDataContainer container = new(dbContext, session.User);
        
        //update flags
        container.Flags = gameResult.Flags;
        
        //remove session
        dbContext.Sessions.Remove(session);
        
        //write to database
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new ResponseContainer
        {
            Result = 200,
            Response = new ResponseBase()
        };
    }
}