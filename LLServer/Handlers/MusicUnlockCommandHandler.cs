using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record MusicUnlockCommand(RequestBase request) : IRequest<ResponseContainer>;

public class MusicUnlockCommandHandler : IRequestHandler<MusicUnlockCommand, ResponseContainer>
{
    private ApplicationDbContext dbContext;
    private readonly SessionHandler sessionHandler;
    
    public MusicUnlockCommandHandler(ApplicationDbContext dbContext, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.sessionHandler = sessionHandler;
    }

    public async Task<ResponseContainer> Handle(MusicUnlockCommand command, CancellationToken cancellationToken)
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

        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.Musics)
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return StaticResponses.EmptyResponse;
        }

        string paramJson = command.request.Param.Value.GetRawText();
        
        //get music unlock data
        MusicUnlockParam? musicUnlockData = JsonSerializer.Deserialize<MusicUnlockParam>(paramJson);
        
        if (musicUnlockData is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //get user data
        PersistentUserDataContainer container = new(dbContext, session);
        
        //add the newly unlocked music id
        container.Musics.Add(new MusicData
        {
            MusicId = musicUnlockData.MusicId,
            Unlocked = true,
            New = true
        });

        return new ResponseContainer
        {
            Result = 200,
            Response = new MusicUnlockResponse
            {
                Musics = container.Musics
            }
        };
    }
}