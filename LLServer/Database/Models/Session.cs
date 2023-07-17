namespace LLServer.Database.Models;

public class Session
{
    public string SessionId { get; set; } = Guid.Empty.ToString("N");

    public ulong UserId { get; set; }

    public DateTime CreateTime { get; set; } = DateTime.MinValue;

    public DateTime ExpireTime { get; set; } = DateTime.MinValue;

    public bool IsActive { get; set; }
}