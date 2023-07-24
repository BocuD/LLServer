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
        var session = await dbContext.Sessions
            .AsSplitQuery()
            .Where(s => s.SessionId == command.request.SessionKey)
            .Select(s => new
            {
                Session = s,
                User = s.User,
                UserData = s.User.UserData,
                UserDataAqours = s.User.UserDataAqours,
                UserDataSaintSnow = s.User.UserDataSaintSnow,
                Members = s.User.Members,
                MemberCards = s.User.MemberCards,
                LiveDatas = s.User.LiveDatas,
                TravelData = s.User.TravelData,
                TravelPamphlets = s.User.TravelPamphlets,
                TravelHistory = s.User.TravelHistory,
                TravelHistoryAqours = s.User.TravelHistoryAqours,
                TravelHistorySaintSnow = s.User.TravelHistorySaintSnow,
                YellAchievements = s.User.YellAchievements,
                AchievementRecordBooks = s.User.AchievementRecordBooks,
                Items = s.User.Items,
                SpecialItems = s.User.SpecialItems,
                NamePlates = s.User.NamePlates,
                Badges = s.User.Badges
            }).FirstOrDefaultAsync(cancellationToken);
        
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