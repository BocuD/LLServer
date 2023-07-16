using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class TravelPamphlet
{
    [JsonPropertyName("m_travel_pamphlet_id")]
    public int TravelPamphletId { get; set; } 

    [JsonPropertyName("round")]
    public int Round { get; set; } 

    [JsonPropertyName("total_talk_count")]
    public int TotalTalkCount { get; set; } 

    [JsonPropertyName("total_dice_count")]
    public int TotalDiceCount { get; set; }

    [JsonPropertyName("is_new")]
    public bool IsNew { get; set; }

    [JsonPropertyName("travel_ex_rewards")]
    public int[] TravelExRewards { get; set; } = Array.Empty<int>();
}