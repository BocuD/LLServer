using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class LimitedAchievement
{
    [JsonPropertyName("m_limited_achievement_id")]
    public int LimitedAchievementId { get; set; } 
    
    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; } 
    [JsonPropertyName("new")]
    public bool New { get; set; } 
}