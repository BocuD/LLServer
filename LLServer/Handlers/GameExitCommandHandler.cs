using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record GameExitCommand(RequestBase request) : BaseRequest(request);

/*
{
    "param": {
        "achievements": [],
        "badges": [],
        "flags": "00000000010110001000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000",
        "honors": [],
        "limited_achievements": [],
        "lives": [
            10000,
            10001,
            10002,
            <etc>
        ],
        "membercard": [],
        "members": [],
        "memorialcard": [],
        "musics": [
            10,
            20,
            30,
            <etc>
        ],
        "nameplates": [],
        "skillcard": [],
        "stages": []
    },
    "protocol": "gameexit",
}
 */

public class GameExitCommandHandler : BaseHandler<GameExitParam, GameExitCommand>
{
    public GameExitCommandHandler(ApplicationDbContext dbContext, ILogger<BaseHandler<GameExitParam, GameExitCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(GameSession session, GameExitParam gameResult, CancellationToken cancellationToken)
    {
        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.Achievements)
                .Include(u => u.LiveDatas)
                .Include(u => u.Members)
                .Include(u => u.MemberCards)
                .Include(u => u.Musics)
                .Include(u => u.NamePlates)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session);
        
        //update achievements
        foreach (int achievement in gameResult.Achievements)
        {
            container.Achievements.Add(new Achievement
            {
                AchievementId = achievement,
                New = true,
                Unlocked = true
            });
        }

        //update flags
        container.Flags = gameResult.Flags;
        
        //todo update honors
        //todo update limited achievements
        
        //todo update lives (currently its unclear what the intended behavior is)
        /*foreach (int liveId in gameResult.Lives)
        {
            container.PersistentLives.Add(new PersistentLiveData
            {
                LiveId = liveId,
                New = true,
                Unlocked = true
            });
        }*/
        
        //update member cards
        foreach (int membercard in gameResult.MemberCards)
        {
            //try to get an existing entry for the card
            MemberCardData? existingEntry = container.MemberCards.FirstOrDefault(c => c.CardMemberId == membercard);
            if (existingEntry is not null)
            {
                //increment the count
                existingEntry.Count++;
                continue;
            }
            
            //create a new entry if one doesn't exist yet
            container.MemberCards.Add(new MemberCardData()
            {
                CardMemberId = membercard,
                Count = 1,
                New = true
            });
        }
        
        //todo update members
        
        //todo update memorial cards
        
        //todo update musics (currently its unclear what the intended behavior is)
        /*foreach (int music in gameResult.Musics)
        {
            container.Musics.Add(new MusicData()
            {
                MusicId = music,
                New = true,
                Unlocked = true
            });
        }*/
        
        //update name plates
        foreach (int nameplate in gameResult.Nameplates)
        {
            container.NamePlates.Add(new NamePlate()
            {
                NamePlateId = nameplate,
                New = true
            });
        }
        
        //todo update skill cards
        
        //todo update stages
        
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