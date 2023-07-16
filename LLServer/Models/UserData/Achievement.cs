using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class Achievement
{
    [JsonPropertyName("m_achievement_id")]
    public int AchievementId { get; set; }

    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; } 

    [JsonPropertyName("new")]
    public bool New { get; set; }
}