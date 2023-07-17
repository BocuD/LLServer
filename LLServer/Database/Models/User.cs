namespace LLServer.Database.Models;

public class User
{
    public ulong UserId { get; set; }

    public string CardId { get; set; } = "7020392000000000";

    public string Name { get; set; } = "";

    public Session? Session { get; set; }
}