using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Mappers;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

public record GetUserDataQuery(RequestBase request) : IRequest<ResponseContainer>;

public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<GetUserDataQueryHandler> logger;

    public GetUserDataQueryHandler(ApplicationDbContext dbContext, ILogger<GetUserDataQueryHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(GetUserDataQuery query, CancellationToken cancellationToken)
    {
        //get user data from db
        Session? session = await dbContext.Sessions
            .Where(s => s.SessionId == query.request.SessionKey)
            .FirstOrDefaultAsync(cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        User user;
        
        if (session.IsGuest)
        {
            user = User.GuestUser;
        }
        else
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.UserData)
                .Include(u => u.UserDataAqours)
                .Include(u => u.UserDataSaintSnow)
                .Include(u => u.Members)
                .Include(u => u.MemberCards)
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
                .FirstOrDefaultAsync(cancellationToken);
            
            user = session.User;
        }
        
        if(user is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //get persistent userdata container
        PersistentUserDataContainer container = new(dbContext, user);

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