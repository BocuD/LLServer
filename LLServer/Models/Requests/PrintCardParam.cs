using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

/*
    "param": {
        "card_id": 40021,
        "category": 1,
        "count": 1,
        "dice_bonus": 0,
        "reason": 2
    },
    "protocol": "printcard",
 */

public class PrintCardParam
{
    [JsonPropertyName("card_id")]
    public int CardId { get; set; }
    
    [JsonPropertyName("category")]
    public int Category { get; set; }
    
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("dice_bonus")]
    public int DiceBonus { get; set; }
    
    [JsonPropertyName("reason")]
    public int Reason { get; set; }
}