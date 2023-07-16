using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class EventReward
{
    [JsonPropertyName("m_event_id")]
    public int EventId { get; set; } 

    [JsonPropertyName("reward_category")]
    public int RewardCategory { get; set; } 

    [JsonPropertyName("reward_id")]
    public int RewardId { get; set; } 

    [JsonPropertyName("event_point")]
    public int EventPoint { get; set; } 

    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("reward_num")]
    public int RewardNum { get; set; } 
}