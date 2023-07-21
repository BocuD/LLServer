using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global

namespace LLServer.Handlers;

public record AuthCommand(JsonElement? Param) : IRequest<ResponseContainer>;

public class AuthCommandHandler : IRequestHandler<AuthCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<AuthCommandHandler> logger;

    public AuthCommandHandler(ApplicationDbContext dbContext, ILogger<AuthCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        if (request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //deserialize from param
        var paramJson = request.Param.Value.GetRawText();

        var authParam = JsonSerializer.Deserialize<AuthParam>(paramJson);

        if (authParam is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        var user = await dbContext.Users
            .Include(u => u.UserData)
            .FirstOrDefaultAsync(u => u.CardId == authParam.NesicaId, cancellationToken);
        
        if (user is null)
        {
            logger.LogInformation("User with nesica id {NesicaId} not found, creating new user", authParam.NesicaId);
            user = await RegisterNewUser(authParam.NesicaId);
        }
        else
        {
            logger.LogInformation("User with nesica id {NesicaId} found", authParam.NesicaId);
        }

        AuthResponse authResponse = new()
        {
            BlockSequence = 1,
            Status = user.Initialized ? 0 : 1,
            Name = user.UserData.Name,
            AbnormalEnd = 0,
            UserId = user.UserId.ToString()
        };

        // Try to find existing session
        var existingSession = await dbContext.Sessions
            .FirstOrDefaultAsync(s => 
                s.UserId == user.UserId, 
            cancellationToken);

        // Make sure the existing session is cleared if it exists
        if (existingSession is not null)
        {
            switch (existingSession.IsActive)
            {
                // Active session not expired
                case true when existingSession.ExpireTime > DateTime.Now:
                    logger.LogWarning("Existing session is still valid for session Id {SessionId}", 
                        existingSession.SessionId);
                    dbContext.Sessions.Remove(existingSession);
                    authResponse.AbnormalEnd = 1;
                    break;
                // Expired or not active
                case true when existingSession.ExpireTime <= DateTime.Now:
                case false:
                    dbContext.Sessions.Remove(existingSession);
                    break;
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
        
        var session = await GenerateNewSession(user, cancellationToken);
        
        //get username from persistent data container
        PersistentUserDataContainer container = new(dbContext, user);
        
        authResponse.Name = container.UserData.Name;
        authResponse.SessionKey = session.SessionId;

        return new ResponseContainer
        {
            Result = 200,
            Response = authResponse
        };
    }
    
    private async Task<User> RegisterNewUser(string nesicaId)
    {
        User user = new()
        {
            CardId = nesicaId,
            Initialized = false,
            
            UserData = new UserData(),
            UserDataAqours = new UserDataAqours(),
            UserDataSaintSnow = new UserDataSaintSnow(),
            
            Members = new List<MemberData>(),
            MemberCards = new List<MemberCardData>(),
            
            LiveDatas = new List<PersistentLiveData>(),
            
            TravelData = new List<TravelData>(),
            TravelPamphlets = new List<TravelPamphlet>(),
            
            TravelHistory = new List<TravelHistory>(),
            TravelHistoryAqours = new List<TravelHistoryAqours>(),
            TravelHistorySaintSnow = new List<TravelHistorySaintSnow>(),
            
            YellAchievements = new List<YellAchievement>(),
            AchievementRecordBooks = new List<AchievementRecordBook>(),
            
            Items = new List<Item>(),
            SpecialItems = new List<SpecialItem>()
        };
        
        dbContext.Users.Add(user);
        
        dbContext.UserData.Add(user.UserData);
        dbContext.UserDataAqours.Add(user.UserDataAqours);
        dbContext.UserDataSaintSnow.Add(user.UserDataSaintSnow);
        
        dbContext.MemberData.AddRange(user.Members);
        dbContext.MemberCardData.AddRange(user.MemberCards);
        
        dbContext.LiveDatas.AddRange(user.LiveDatas);
        
        dbContext.TravelData.AddRange(user.TravelData);
        dbContext.TravelPamphlets.AddRange(user.TravelPamphlets);
        
        dbContext.TravelHistory.AddRange(user.TravelHistory);
        dbContext.TravelHistoryAqours.AddRange(user.TravelHistoryAqours);
        dbContext.TravelHistorySaintSnow.AddRange(user.TravelHistorySaintSnow);
        
        dbContext.YellAchievements.AddRange(user.YellAchievements);
        dbContext.AchievementRecordBooks.AddRange(user.AchievementRecordBooks);
        
        dbContext.Items.AddRange(user.Items);
        dbContext.SpecialItems.AddRange(user.SpecialItems);

        await dbContext.SaveChangesAsync();
        
        return user;
    }

    private async Task<Session> GenerateNewSession(User user, CancellationToken cancellationToken)
    {
        var session = new Session
        {
            SessionId = Guid.NewGuid().ToString("N"),
            UserId = user.UserId,
            CreateTime = DateTime.Now,
            IsActive = false,
            User = user
        };
        dbContext.Sessions.Add(session);
        await dbContext.SaveChangesAsync(cancellationToken);
        return session;
    }
}