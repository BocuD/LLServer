using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Responses;

public class GameTotalResultResponse : ResponseBase
{
    [JsonPropertyName("cheer")] 
    public CheerInfo Cheer { get; set; }
    
    [JsonPropertyName("item")]
    public Item[] Items { get; set; } = Array.Empty<Item>();
}

public class CheerInfo
{
    [JsonPropertyName("yell_count")]
    public int YellCount { get; set; }
    
    [JsonPropertyName("cheer_rank")]
    public int CheerRank { get; set; }
    
    [JsonPropertyName("reward_mobile_point")]
    public int RewardMobilePoint { get; set; }
    
    [JsonPropertyName("now")]
    public string Now { get; set; } //yyyy-MM-ddHH:mm:ss
    
    [JsonPropertyName("cheer_players")]
    public List<CheerPlayer> CheerPlayers { get; set; } = new();
}

public class CheerPlayer
{
    [JsonPropertyName("m_stamp_id")]
    public int StampId { get; set; }
    
    [JsonPropertyName("yell_rank")]
    public int YellRank { get; set; }
    
    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("nameplate")]
    public int Nameplate { get; set; }
    
    [JsonPropertyName("badge")]
    public int Badge { get; set; }
}