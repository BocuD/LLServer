using System.Text.Json.Serialization;

namespace LLServer.Models.Travel;

public class TravelOtherPlayerData
{
    //todo: probably player id, int128 20 characters
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = string.Empty;
    
    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; }
    
    [JsonPropertyName("positions")]
    public int[] Positions { get; set; } = new int[3];
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("level")]
    public int Level { get; set; }
    
    [JsonPropertyName("honor")]
    public int Honor { get; set; }
    
    [JsonPropertyName("card_memorial_id")]
    public int CardMemorialId { get; set; }
    
    [JsonPropertyName("badge")]
    public int Badge { get; set; }
    
    [JsonPropertyName("nameplate")]
    public int Nameplate { get; set; }
    
    [JsonPropertyName("yell_rank")]
    public int YellRank { get; set; }
    
    [JsonPropertyName("tenpo_name")]
    public string TenpoName { get; set; } = string.Empty;
}