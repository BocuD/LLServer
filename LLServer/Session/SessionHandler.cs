using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Session;

public interface ISessionHandler
{
    public Task<GameSession> GenerateNewSession(User user, CancellationToken cancellationToken, bool isTerminal = false, bool isGuestSession = false);
}

public class SessionHandler : ISessionHandler
{
    private readonly ApplicationDbContext dbContext;
    
    public SessionHandler (ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<GameSession> GenerateNewSession(User user, CancellationToken cancellationToken, bool isTerminal = false, bool isGuestSession = false)
    {
        var session = new GameSession
        {
            SessionId = Guid.NewGuid().ToString("N"),
            CreateTime = DateTime.UtcNow,
            IsActive = false,
            IsGuest = isGuestSession
        };

        if (!isGuestSession)
        {
            session.UserId = user.UserId;
            session.User = user;
        }

        session.IsTerminal = isTerminal;

        dbContext.Sessions.Add(session);
        await dbContext.SaveChangesAsync(cancellationToken);
        return session;
    }

    public async Task<GameSession?> GetSession(RequestBase request, CancellationToken cancellationToken)
    {
        return await dbContext.Sessions
            .Where(s => s.SessionId == request.SessionKey)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
