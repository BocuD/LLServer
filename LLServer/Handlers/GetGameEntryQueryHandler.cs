using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Mappers;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using MediatR;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

public record GetGameEntryQuery(RequestBase request) : IRequest<ResponseContainer>;

//todo: migrate to BaseHandler and implement params for both gameentry and gameentry.center
public class GetGameEntryQueryHandler : IRequestHandler<GetGameEntryQuery, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<GetGameEntryQueryHandler> logger;
    private readonly SessionHandler sessionHandler;

    public GetGameEntryQueryHandler(ApplicationDbContext dbContext, ILogger<GetGameEntryQueryHandler> logger,
        SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }

    public async Task<ResponseContainer> Handle(GetGameEntryQuery query, CancellationToken cancellationToken)
    {
        GameSession? session = await sessionHandler.GetSession(query.request, cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //todo: this seems to not be working
        // Mark the session as active and set the expire time
        session.IsActive = true;
        session.ExpireTime = DateTime.Now.AddMinutes(60);

        if (session.IsTerminal)
        {
            return await TerminalGameEntryResponse(cancellationToken, session);
        }
        else
        {
            if (session.IsGuest)
            {
                return await SatelliteGuestGameEntryResponse(cancellationToken, session);
            }
            else
            {
                return await SatelliteGameEntryResponse(cancellationToken, session);
            }
        }
    }

    private async Task<ResponseContainer> TerminalGameEntryResponse(CancellationToken cancellationToken,
        GameSession session)
    {
        //terminal is special and wants the full userdata for some reason :P
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
            .Include(u => u.TravelHistoryAqours)
            .Include(u => u.TravelHistorySaintSnow)
            .Include(u => u.Achievements)
            .Include(u => u.YellAchievements)
            .Include(u => u.AchievementRecordBooks)
            .Include(u => u.Items)
            .Include(u => u.SpecialItems)
            .Include(u => u.NamePlates)
            .Include(u => u.Badges)
            .Include(u => u.Musics)
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

    private async Task<ResponseContainer> SatelliteGuestGameEntryResponse(CancellationToken cancellationToken,
        GameSession session)
    {
        PersistentUserDataContainer container = new(dbContext, session);

        //response
        GameEntryResponseMapper mapper = new();
        GameEntryResponse response = mapper.FromPersistentUserData(container);

        //copy over userdata and userdataaqours manually so we can safely modify them
        response.UserData = new UserData();
        response.UserDataAqours = new UserDataAqours();
        response.UserData = ReflectionMapper.Map(container.UserData, response.UserData);
        response.UserDataAqours = ReflectionMapper.Map(container.UserDataAqours, response.UserDataAqours);

        response.UserData.CharacterId = 1;
        response.UserDataAqours.CharacterId = 11;

        return new ResponseContainer
        {
            Result = 200,
            Response = response
        };
    }

    private async Task<ResponseContainer> SatelliteGameEntryResponse(CancellationToken cancellationToken,
        GameSession session)
    {
        //load user from db
        session.User = await dbContext.Users
            .Where(u => u.UserId == session.UserId)
            .AsSplitQuery()
            .Include(u => u.UserData)
            .Include(u => u.UserDataAqours)
            .Include(u => u.UserDataSaintSnow)
            .Include(u => u.Members)
            .Include(u => u.MemberCards)
            .Include(u => u.SkillCards)
            .Include(u => u.TravelPamphlets)
            .Include(u => u.Items)
            .Include(u => u.SpecialItems)
            .FirstOrDefaultAsync(cancellationToken);

        PersistentUserDataContainer container = new(dbContext, session);

        container.UserData.PlaySatellite++;

        //update last play time (format is "2023-07-02 00:00:00")
        container.UserData.PlayDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        await container.SaveChanges(cancellationToken);

        //response
        GameEntryResponseMapper mapper = new();
        GameEntryResponse response = mapper.FromPersistentUserData(container);

        //todo: this is a temporary solution to add skill card functionality
        //check if the user has any skill cards; if not add them and the remaining default character cards
        if (container.SkillCards.Count == 0)
        {
            //add default skill cards for all members
            container.SkillCards.AddRange(SkillCardData.InitialSkillCards.Select(x => new SkillCardData
            {
                CardSkillId = x,
                SkillLevel = 1,
                New = true
            }));

            //add default cards for all members
            container.MemberCards.AddRange(MemberCardData.InitialMemberCards
                .Where(x => x != 0 && container.MemberCards.All(y => y.CardMemberId != x)).Select(x =>
                    new MemberCardData
                    {
                        CardMemberId = x,
                        Count = 1,
                        New = true
                    }));

            await container.SaveChanges(cancellationToken);
        }

        return new ResponseContainer
        {
            Result = 200,
            Response = response
        };
    }
}