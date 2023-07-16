using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class Badge
{
    [JsonPropertyName("m_badge_id")]
    public int BadgeId { get; set; }

    [JsonPropertyName("new")]
    public bool New { get; set; }
}