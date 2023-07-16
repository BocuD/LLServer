using System.Text.Json.Serialization;

namespace LLServer.Models.UserData;

public class CoopInfo
{
    [JsonPropertyName("coop_player_name")]
    public string CoopPlayerName { get; set; } = "";

    [JsonPropertyName("coop_player_m_nameplate_id")]
    public int CoopPlayerMNameplateId { get; set; } = 0;

    [JsonPropertyName("coop_player_m_badge_id")]
    public int CoopPlayerMBadgeId { get; set; } = 0;
}