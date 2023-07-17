using System.Security.Cryptography;
using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
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
        
        var authResponse = new AuthResponse
        {
            BlockSequence = 1,
            Status = 0,
            Name = "Test123456",
            AbnormalEnd = 0
        };

        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.CardId == authParam.NesicaId, 
            cancellationToken);
        if (user is null)
        {
            user = await RegisterNewUser(authParam.NesicaId);
            authResponse.Status = 1;
            authResponse.Name = user.Name;
        }
        authResponse.UserId = user.UserId.ToString();

        var existingSession = await dbContext.Sessions
            .FirstOrDefaultAsync(s => 
                s.UserId == user.UserId, 
            cancellationToken);

        // No existing session
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
        };
        
        dbContext.Users.Add(user);
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