using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class YellAchievement
{
    [JsonPropertyName("m_yell_achievement_id")]
    public int YellAchievementId { get; set; }

    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; }

    [JsonPropertyName("new")]
    public bool New { get; set; }
}