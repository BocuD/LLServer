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

public class GetGameEntryQueryHandler : IRequestHandler<GetGameEntryQuery, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<GetGameEntryQueryHandler> logger;
    private readonly SessionHandler sessionHandler;

    public GetGameEntryQueryHandler(ApplicationDbContext dbContext, ILogger<GetGameEntryQueryHandler> logger, SessionHandler sessionHandler)
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

        if (!session.IsGuest)
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
                .Include(u => u.TravelPamphlets)
                .Include(u => u.Items)
                .Include(u => u.SpecialItems)
                .FirstOrDefaultAsync(cancellationToken);
        }

        PersistentUserDataContainer container = new(dbContext, session);
        
        container.UserData.PlaySatellite++;
        
        //update last play time (format is "2023-07-02 00:00:00")
        container.UserData.PlayDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        await container.SaveChanges(cancellationToken);

        //response
        GameEntryResponseMapper mapper = new();
        GameEntryResponse response = mapper.FromPersistentUserData(container);
        
        //copy over userdata and userdataaqours manually so we can safely modify them
        response.UserData = new UserData();
        response.UserDataAqours = new UserDataAqours();
        response.UserData = ReflectionMapper.Map(container.UserData, response.UserData);
        response.UserDataAqours = ReflectionMapper.Map(container.UserDataAqours, response.UserDataAqours);
        
        //this prevents the character selection screen from showing up in guest mode
        if (session.IsGuest)
        {
            response.UserData.CharacterId = 1;
            response.UserDataAqours.CharacterId = 11;
        }

        return new ResponseContainer
        {
            Result = 200,
            Response = response
        };
    }
}