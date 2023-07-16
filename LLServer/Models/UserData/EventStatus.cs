using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class EventStatus
{
    [JsonPropertyName("m_event_id")]
    public int EventId { get; set; } 

    [JsonPropertyName("event_point")]
    public int EventPoint { get; set; }

    [JsonPropertyName("next_reward")]
    public int NextReward { get; set; } 

    [JsonPropertyName("level")]
    public int Level { get; set; } 

    [JsonPropertyName("rank")]
    public int Rank { get; set; } 

    [JsonPropertyName("first_play")]
    public int FirstPlay { get; set; } 
}