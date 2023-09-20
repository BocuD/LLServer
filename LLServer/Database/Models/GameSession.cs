namespace LLServer.Database.Models;

public class GameSession
{
    public string SessionId { get; set; } = Guid.Empty.ToString("N");

    public ulong? UserId { get; set; }

    public DateTime CreateTime { get; set; } = DateTime.MinValue;

    public DateTime ExpireTime { get; set; } = DateTime.MinValue;

    public bool IsActive { get; set; }

    public bool IsGuest { get; set; }
    public bool IsTerminal { get; set; }

    public User? User { get; set; }
}