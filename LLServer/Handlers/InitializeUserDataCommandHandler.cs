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

public record InitializeUserDataCommand(RequestBase request) : IRequest<ResponseContainer>;

public class InitializeUserDataCommandHandler : IRequestHandler<InitializeUserDataCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<InitializeUserDataCommandHandler> logger;

    public InitializeUserDataCommandHandler(ApplicationDbContext dbContext, ILogger<InitializeUserDataCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(InitializeUserDataCommand command, CancellationToken cancellationToken)
    {
        if (command.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        var paramJson = command.request.Param.Value.GetRawText();

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
                Achievements = s.User.Achievements,
                YellAchievements = s.User.YellAchievements,
                AchievementRecordBooks = s.User.AchievementRecordBooks,
                Items = s.User.Items,
                SpecialItems = s.User.SpecialItems
            }).FirstOrDefaultAsync(cancellationToken);
        
        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //get the initialize command
        InitializeUserData? initializeUserData = JsonSerializer.Deserialize<InitializeUserData>(paramJson);
        if (initializeUserData is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //write to db
        PersistentUserDataContainer container = new(dbContext, session.User);

        container.Initialize(initializeUserData);
        session.User.Initialized = true;
        
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