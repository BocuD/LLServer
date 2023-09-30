using LLServer.Common;
using LLServer.Database;
using LLServer.Mappers;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

public record SetUserDataCommand(RequestBase request) : BaseRequest(request);

public class SetUserDataCommandHandler : ParamHandler<SetUserDataParam, SetUserDataCommand>
{
    public SetUserDataCommandHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<SetUserDataParam, SetUserDataCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(SetUserDataParam setUserData, CancellationToken cancellationToken)
    {
        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.UserData)
                .Include(u => u.UserDataAqours)
                .Include(u => u.UserDataSaintSnow)
                .Include(u => u.Members)
                .Include(u => u.MemberCards)
                .Include(u => u.SkillCards)
                .Include(u => u.LiveDatas)
                .Include(u => u.TravelData)
                .Include(u => u.TravelPamphlets)
                .Include(u => u.TravelHistory)
                .Include(u => u.Achievements)
                .Include(u => u.YellAchievements)
                .Include(u => u.AchievementRecordBooks)
                .Include(u => u.Items)
                .Include(u => u.SpecialItems)
                .Include(u => u.NamePlates)
                .Include(u => u.Badges)
                .Include(u => u.Honors)
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return StaticResponses.EmptyResponse;
        }
        
        //write to db
        PersistentUserDataContainer container = new(dbContext, session);

        if (!session.IsGuest)
        {
            container.SetUserData(setUserData);

            await container.SaveChanges(cancellationToken);
        }

        //response
        UserDataResponseMapper mapper = new();

        return new ResponseContainer
        {
            Result = 200,
            Response = mapper.FromPersistentUserData(container)
        };
    }
}