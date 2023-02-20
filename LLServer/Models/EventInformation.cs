using System.Text.Json.Serialization;

namespace LLServer.Models;

public class EventInformation
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("active")]
    public required bool Active { get; set; }
    
    [JsonPropertyName("event_type")]
    public required int EventType { get; set; }
    
    [JsonPropertyName("point_type")]
    public required int PointType { get; set; }
    
    [JsonPropertyName("character_id")]
    public required int CharacterId { get; set; }
    
    [JsonPropertyName("point_mag")]
    public required int PointMag { get; set; }
    
    [JsonPropertyName("m_travel_pamphlet_id")]
    public required int MemberTravelPamphletId { get; set; }
    
    [JsonPropertyName("start_datetime")]
    public required DateTime StartDatetime { get; set; }
    
    [JsonPropertyName("end_datetime")]
    public required DateTime EndDatetime { get; set; }
    
    /// <summary>
    /// Max length 30
    /// </summary>
    [JsonPropertyName("res_banner")]
    public required string ResBanner { get; set; }
    
    /// <summary>
    /// Max length 30
    /// </summary>
    [JsonPropertyName("res_reward")]
    public required string ResReward { get; set; }
    
    /// <summary>
    /// Max length 62
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("musics")] 
    public List<int> Musics { get; set; } = new();

    [JsonPropertyName("levels")] 
    public List<int> Levels { get; set; } = new();

    [JsonPropertyName("rewards")] public List<EventReward> EventRewards { get; set; } = new();
}

public class EventReward
{
    [JsonPropertyName("order")]
    public required int Order { get; set; }
    
    [JsonPropertyName("id")]
    public required int Id { get; set; }
    
    [JsonPropertyName("require_point")]
    public required int RequirePoint { get; set; }
    
    [JsonPropertyName("reward_type")]
    public required int RewardType { get; set; }
    
    [JsonPropertyName("reward_arg")]
    public required int RewardArg { get; set; }
}