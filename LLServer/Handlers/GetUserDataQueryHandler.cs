using System.Text.Json;
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
        //get session
        Session? session = await dbContext.Sessions
            .Include(s => s.User)
            .Include(s => s.User.UserData)
            .Include(s => s.User.UserDataAqours)
            .Include(s => s.User.UserDataSaintSnow)
            .Include(s => s.User.Members)
            .Include(s => s.User.MemberCards)
            .Include(s => s.User.LiveDatas)
            .FirstOrDefaultAsync(s => s.SessionId == query.request.SessionKey, cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //get persistent userdata container
        PersistentUserDataContainer container = new(dbContext, session.User);
        
        //log userdata, userdata_aqours, userdata_saintsnow
        logger.LogInformation("UserData: {@UserData}", JsonSerializer.Serialize(container.UserData));
        logger.LogInformation("UserDataAqours: {@UserDataAqours}", JsonSerializer.Serialize(container.UserDataAqours));
        logger.LogInformation("UserDataSaintSnow: {@UserDataSaintSnow}", JsonSerializer.Serialize(container.UserDataSaintSnow));
        
        //response
        UserDataResponseMapper mapper = new();
        UserDataResponse response = mapper.FromPersistentUserData(container);

        return new ResponseContainer
        {
            Result = 200,
            Response = response
        };
    }
}