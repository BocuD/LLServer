using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class TravelHistory
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("m_card_member_id")]
    public int CardMemberId { get; set; } 

    [JsonPropertyName("m_snap_background_id")]
    public int SnapBackgroundId { get; set; } 

    [JsonPropertyName("other_character_id")]
    public int OtherCharacterId { get; set; }

    [JsonPropertyName("other_player_name")]
    public string OtherPlayerName { get; set; } = "";

    [JsonPropertyName("other_player_nameplate")]
    public int OtherPlayerNameplate { get; set; } 

    [JsonPropertyName("other_player_badge")]
    public int OtherPlayerBadge { get; set; } 

    [JsonPropertyName("m_travel_pamphlet_id")]
    public int TravelPamphletId { get; set; } 

    [JsonPropertyName("create_type")]
    public int CreateType { get; set; }

    [JsonPropertyName("tenpo_name")]
    public string TenpoName { get; set; } = "";

    [JsonPropertyName("snap_stamp_list")]
    public SnapStamp[] SnapStampList { get; set; } = Array.Empty<SnapStamp>();

    [JsonPropertyName("coop_info")]
    public CoopInfo[] CoopInfo { get; set; } = Array.Empty<CoopInfo>();

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";

    [JsonPropertyName("print_rest")]
    public bool PrintRest { get; set; } 
}