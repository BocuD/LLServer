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

public record GetGameEntryQuery(RequestBase request) : IRequest<ResponseContainer>;

public class GetGameEntryQueryHandler : IRequestHandler<GetGameEntryQuery, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<GetGameEntryQueryHandler> logger;

    public GetGameEntryQueryHandler(ApplicationDbContext dbContext, ILogger<GetGameEntryQueryHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(GetGameEntryQuery query, CancellationToken cancellationToken)
    {
        //get user data from db
        Session? session = await dbContext.Sessions
            .Where(s => s.SessionId == query.request.SessionKey)
            .FirstOrDefaultAsync(cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //todo: this seems to not be working
        // Mark the session as active and set the expire time
        session.IsActive = true;
        session.ExpireTime = DateTime.Now.AddMinutes(60);

        User? user;

        if (session.IsGuest)
        {
            user = User.GuestUser;
        }
        else
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
            
            user = session.User;
        }

        if (user == null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        PersistentUserDataContainer container = new(dbContext, user);
        
        container.UserData.PlaySatellite++;
        
        //update last play time (format is "2023-07-02 00:00:00")
        container.UserData.PlayDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        await dbContext.SaveChangesAsync(cancellationToken);

        //response
        GameEntryResponseMapper mapper = new();

        return new ResponseContainer
        {
            Result = 200,
            Response = mapper.FromPersistentUserData(container)
        };
    }
}