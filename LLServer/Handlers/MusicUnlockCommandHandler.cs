using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record MusicUnlockCommand(RequestBase request) : BaseRequest(request);

public class MusicUnlockCommandHandler : BaseHandler<MusicUnlockParam, MusicUnlockCommand>
{
    public MusicUnlockCommandHandler(ApplicationDbContext dbContext, ILogger<BaseHandler<MusicUnlockParam, MusicUnlockCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(GameSession session, MusicUnlockParam musicUnlockData, CancellationToken cancellationToken)
    {
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