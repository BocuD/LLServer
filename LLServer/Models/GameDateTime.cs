using System.Globalization;

namespace LLServer.Models;

public class GameDateTime
{
    public DateTime DateTime
    {
        get => DateTime.ParseExact(DateTimeString, "yyyy-MM-ddHH:mm:ss", CultureInfo.InvariantCulture);
        set => DateTimeString = value.ToString("yyyy-MM-ddHH:mm:ss");
    }
    
    public string DateTimeString { get; set; } = "";
}