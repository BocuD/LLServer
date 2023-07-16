using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class MissionPoint
{
    [JsonPropertyName("point")]
    public int Point { get; set; } 

    [JsonPropertyName("achieved_point")]
    public int AchievedPoint { get; set; } 
}