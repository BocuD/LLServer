using System.Text.Json.Serialization;

namespace LLServer.Models.Responses.Gacha;

public class MemberGachaResponse : ResponseBase
{
    [JsonPropertyName("m_gacha_id")]
    public int GachaId { get; set; }
    
    [JsonPropertyName("last_crackers")] //max size 3
    public int[] LastCrackers { get; set; } = new int[0];
    
    [JsonPropertyName("box")] //max size 9
    public GachaBox[] Box { get; set; } = new GachaBox[0];
}

public class GachaBox
{
    [JsonPropertyName("category")]
    public int Category { get; set; }
    
    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; }
    
    [JsonPropertyName("item_id")]
    public int ItemId { get; set; }
    
    [JsonPropertyName("level")]
    public int Level { get; set; }
    
    [JsonPropertyName("enable")]
    public int Enable { get; set; }
}