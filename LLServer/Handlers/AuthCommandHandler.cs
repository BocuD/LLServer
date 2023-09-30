using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using MediatR;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global

namespace LLServer.Handlers;

public record AuthCommand(RequestBase request) : IRequest<ResponseContainer>;

public class AuthCommandHandler : IRequestHandler<AuthCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<AuthCommandHandler> logger;
    private readonly SessionHandler sessionHandler;

    public AuthCommandHandler(ApplicationDbContext dbContext, ILogger<AuthCommandHandler> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }

    public async Task<ResponseContainer> Handle(AuthCommand command, CancellationToken cancellationToken)
    {
        if (command.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //deserialize from param
        string paramJson = command.request.Param.Value.GetRawText();

        var authParam = JsonSerializer.Deserialize<AuthParam>(paramJson);

        if (authParam is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        if (authParam.GuestFlag == 1)
        {
            GameSession guestSession = await sessionHandler.GenerateNewSession(User.GuestUser, cancellationToken, false, true);

            return new ResponseContainer
            {
                Result = 200,
                Response = new AuthResponse
                {
                    BlockSequence = 1,
                    Status = 0,
                    Name = "ゲストプレイヤー",
                    AbnormalEnd = 0,
                    UserId = "1",
                    SessionKey = guestSession.SessionId,
                }
            };
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
        GameSession? existingSession = await dbContext.Sessions
            .FirstOrDefaultAsync(s => s.UserId == user.UserId, cancellationToken);

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
        
        GameSession session = await sessionHandler.GenerateNewSession(user, cancellationToken, command.request.Terminal.TerminalAttrib == 1);
        
        //get username from persistent data container
        PersistentUserDataContainer container = new(dbContext, session);
        
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
            SkillCards = new List<SkillCardData>(),
            MemorialCards = new List<MemorialCardData>(),
            
            LiveDatas = new List<PersistentLiveData>(),
            
            TravelData = new List<TravelData>(),
            TravelPamphlets = new List<TravelPamphlet>(),
            
            GameHistory = new List<GameHistory>(),
            TravelHistory = new List<TravelHistory>(),
            
            Achievements = new List<Achievement>(),
            YellAchievements = new List<YellAchievement>(),
            AchievementRecordBooks = new List<AchievementRecordBook>(),
            
            Items = new List<Item>(),
            SpecialItems = new List<SpecialItem>(),
            
            CardFrames = new List<CardFrame>(),
            NamePlates = new List<NamePlate>(),
            Badges = new List<Badge>(),
            Honors = new List<HonorData>(),
            
            Musics = new List<MusicData>(),
        };
        
        dbContext.Users.Add(user);
        
        dbContext.UserData.Add(user.UserData);
        dbContext.UserDataAqours.Add(user.UserDataAqours);
        dbContext.UserDataSaintSnow.Add(user.UserDataSaintSnow);
        
        dbContext.MemberData.AddRange(user.Members);
        dbContext.MemberCardData.AddRange(user.MemberCards);
        dbContext.SkillCardData.AddRange(user.SkillCards);
        dbContext.MemorialCardData.AddRange(user.MemorialCards);
        
        dbContext.LiveDatas.AddRange(user.LiveDatas);
        
        dbContext.TravelData.AddRange(user.TravelData);
        dbContext.TravelPamphlets.AddRange(user.TravelPamphlets);
        
        dbContext.GameHistory.AddRange(user.GameHistory);
        dbContext.TravelHistory.AddRange(user.TravelHistory);
        
        dbContext.Achievements.AddRange(user.Achievements);
        dbContext.YellAchievements.AddRange(user.YellAchievements);
        dbContext.AchievementRecordBooks.AddRange(user.AchievementRecordBooks);
        
        dbContext.Items.AddRange(user.Items);
        dbContext.SpecialItems.AddRange(user.SpecialItems);
        
        dbContext.CardFrames.AddRange(user.CardFrames);
        dbContext.NamePlates.AddRange(user.NamePlates);
        dbContext.Badges.AddRange(user.Badges);
        dbContext.Honors.AddRange(user.Honors);
        
        dbContext.Musics.AddRange(user.Musics);

        await dbContext.SaveChangesAsync();
        
        return user;
    }
}