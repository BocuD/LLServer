using System.Text.Json;
using LLServer.Models.Requests.Travel;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.Travel;
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

    public AuthCommandHandler(ApplicationDbContext dbContext, ILogger<AuthCommandHandler> logger,
        SessionHandler sessionHandler)
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
            GameSession guestSession =
                await sessionHandler.GenerateNewSession(User.GuestUser, cancellationToken, false, true);

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
            user = await dbContext.RegisterNewUser(authParam.NesicaId);
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

        GameSession session =
            await sessionHandler.GenerateNewSession(user, cancellationToken,
                command.request.Terminal.TerminalAttrib == 1);

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
}