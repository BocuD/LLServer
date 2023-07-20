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

public record SetUserDataCommand(RequestBase request) : IRequest<ResponseContainer>;

public class SetUserDataCommandHandler : IRequestHandler<SetUserDataCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<SetUserDataCommandHandler> logger;

    public SetUserDataCommandHandler(ApplicationDbContext dbContext, ILogger<SetUserDataCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(SetUserDataCommand command, CancellationToken cancellationToken)
    {
        if (command.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //get session
        Session? session = await dbContext.Sessions
            .Include(s => s.User)
            .Include(s => s.User.UserData)
            .Include(s => s.User.UserDataAqours)
            .Include(s => s.User.UserDataSaintSnow)
            
            .Include(s => s.User.Members)
            .Include(s => s.User.MemberCards)
            
            .Include(s => s.User.TravelData)
            .Include(s => s.User.TravelPamphlets)
            .Include(s => s.User.TravelHistory)
            .Include(s => s.User.TravelHistoryAqours)
            .Include(s => s.User.TravelHistorySaintSnow)
            
            .Include(s => s.User.AchievementRecordBooks)
            
            .FirstOrDefaultAsync(s => 
                    s.SessionId == command.request.SessionKey, 
                cancellationToken);
        
        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //get setuserdata command
        string paramJson = command.request.Param.Value.GetRawText();

        SetUserData? setUserData = JsonSerializer.Deserialize<SetUserData>(paramJson);
        if (setUserData is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //write to db
        PersistentUserDataContainer container = new(dbContext, session.User);
        
        container.SetUserData(setUserData);

        await dbContext.SaveChangesAsync(cancellationToken);

        //response
        UserDataResponseMapper mapper = new();

        return new ResponseContainer
        {
            Result = 200,
            Response = mapper.FromPersistentUserData(container)
        };
    }
}