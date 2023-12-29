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

public record GetUserDataQuery(RequestBase request) : IRequest<ResponseContainer>;

//todo: implement param handling for GetUserData (it specifies a list of (history?) fields to return, but we just return everything right now)
public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<GetUserDataQueryHandler> logger;
    private readonly SessionHandler sessionHandler;
    
    public GetUserDataQueryHandler(ApplicationDbContext dbContext, ILogger<GetUserDataQueryHandler> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }

    public async Task<ResponseContainer> Handle(GetUserDataQuery query, CancellationToken cancellationToken)
    {
        GameSession? session = await sessionHandler.GetSession(query.request, cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
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
        }

        //get persistent userdata container
        PersistentUserDataContainer container = new(dbContext, session);

        //response
        UserDataResponseMapper mapper = new();
        UserDataResponse response = mapper.FromPersistentUserData(container);
        
        //this will prevent the name entry popup from showing up
        response.Flags.SetFlag(181);
        
        return new ResponseContainer
        {
            Result = 200,
            Response = response
        };
    }
}