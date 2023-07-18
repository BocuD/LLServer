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
            .FirstOrDefaultAsync(s => 
                    s.SessionId == query.request.SessionKey, 
                cancellationToken);
        
        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        // Mark the session as active and set the expire time
        session.IsActive = true;
        session.ExpireTime = DateTime.UtcNow.AddMinutes(60);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        PersistentUserDataContainer container = new(dbContext, session.User);

        //response
        GameEntryResponseMapper mapper = new();

        return new ResponseContainer
        {
            Result = 200,
            Response = mapper.FromPersistentUserData(container)
        };
    }
}