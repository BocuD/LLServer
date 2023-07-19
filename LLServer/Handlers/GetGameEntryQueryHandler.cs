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
            .Include(s => s.User)
            .Include(s => s.User.UserData)
            .Include(s => s.User.UserDataAqours)
            .Include(s => s.User.UserDataSaintSnow)
            .Include(s => s.User.Members)
            .Include(s => s.User.MemberCards)
            .FirstOrDefaultAsync(s => 
                    s.SessionId == query.request.SessionKey, 
                cancellationToken);
        
        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        // Mark the session as active and set the expire time
        session.IsActive = true;
        session.ExpireTime = DateTime.Now.AddMinutes(60);

        PersistentUserDataContainer container = new(dbContext, session.User);
        
        container.UserData.PlaySatellite++;
        
        //update last play time
        container.UserData.PlayDateTime.DateTime = DateTime.Now;
        
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