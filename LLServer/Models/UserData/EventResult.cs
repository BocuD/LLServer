using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class EventResult
{
    [JsonPropertyName("m_event_id")]
    public int EventId { get; set; } 

    [JsonPropertyName("add_point")]
    public int AddPoint { get; set; } 

    [JsonPropertyName("event_point")]
    public int EventPoint { get; set; }

    [JsonPropertyName("update_score")]
    public int UpdateScore { get; set; }

    [JsonPropertyName("rewards")]
    public EventResultReward[] Rewards { get; set; } = Array.Empty<EventResultReward>();
}