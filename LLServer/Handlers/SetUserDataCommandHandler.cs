using System.Text.Json;
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

public record SetUserDataCommand(RequestBase request) : IRequest<ResponseContainer>;

public class SetUserDataCommandHandler : IRequestHandler<SetUserDataCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<SetUserDataCommandHandler> logger;
    private readonly SessionHandler sessionHandler;

    public SetUserDataCommandHandler(ApplicationDbContext dbContext, ILogger<SetUserDataCommandHandler> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }

    public async Task<ResponseContainer> Handle(SetUserDataCommand command, CancellationToken cancellationToken)
    {
        //get user data from db
        GameSession? session = await sessionHandler.GetSession(command.request, cancellationToken);

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
        }
        else
        {
            return StaticResponses.EmptyResponse;
        }

        //get setuserdata command
        string paramJson = command.request.Param.Value.GetRawText();

        SetUserData? setUserData = JsonSerializer.Deserialize<SetUserData>(paramJson);
        if (setUserData is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //write to db
        PersistentUserDataContainer container = new(dbContext, session);

        if (!session.IsGuest)
        {
            container.SetUserData(setUserData);

            await container.SaveChanges(cancellationToken);
        }

        //response
        UserDataResponseMapper mapper = new();

        return new ResponseContainer
        {
            Result = 200,
            Response = mapper.FromPersistentUserData(container)
        };
    }
}