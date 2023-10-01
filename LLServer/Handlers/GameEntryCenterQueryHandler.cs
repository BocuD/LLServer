using LLServer.Database;
using LLServer.Mappers;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

public record GameEntryCenterQuery(RequestBase request) : BaseRequest(request);

public class GameEntryCenterQueryHandler : ParamHandler<GameEntryCenterParam, GameEntryCenterQuery>
{
    public GameEntryCenterQueryHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<GameEntryCenterParam, GameEntryCenterQuery>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(GameEntryCenterParam param, CancellationToken cancellationToken)
    {
        //todo: this seems to not be working
        // Mark the session as active and set the expire time
        session.IsActive = true;
        session.ExpireTime = DateTime.Now.AddMinutes(60);

        //terminal is special and seems to want the full userdata for some reason :P
        session.User = await dbContext.Users
            .Where(u => u.UserId == session.UserId)
            .AsSplitQuery()
            .Include(u => u.UserData)
            .Include(u => u.UserDataAqours)
            .Include(u => u.UserDataSaintSnow)
            
            .Include(u => u.Members)
            .Include(u => u.MemberCards)
            .Include(u => u.SkillCards)
            .Include(u => u.MemorialCards)
            
            .Include(u => u.Musics)
            .Include(u => u.LiveDatas)
            
            .Include(u => u.TravelData)
            .Include(u => u.TravelPamphlets)
            .Include(u => u.TravelTalks)
            
            .Include(u => u.GameHistory)
            .Include(u => u.TravelHistory)
            
            .Include(u => u.Achievements)
            .Include(u => u.YellAchievements)
            .Include(u => u.AchievementRecordBooks)
            .Include(u => u.LimitedAchievements)
            
            .Include(u => u.Items)
            .Include(u => u.SpecialItems)
            
            .Include(u => u.CardFrames)
            .Include(u => u.NamePlates)
            .Include(u => u.Badges)
            .Include(u => u.Honors)
            
            .Include(u => u.MailBox)
            .FirstOrDefaultAsync(cancellationToken);
        
        //get persistent userdata container
        PersistentUserDataContainer container = new(dbContext, session);

        container.UserData.PlayCenter++;

        //update last play time (format is "2023-07-02 00:00:00")
        container.UserData.PlayDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        await container.SaveChanges(cancellationToken);
        
        //response
        UserDataResponseMapper mapper = new();
        UserDataResponse response = mapper.FromPersistentUserData(container);

        //todo: get rid of this
        //stupid hack to prevent the name entry popup and tutorials from showing up
        response.Flags = response.Flags.Replace("0", "1");
        return new ResponseContainer
        {
            Result = 200,
            Response = response
        };
    }
}