using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

/*
    "param": {
        "card_count": 1,
        "cracker_count": 1,
        "credit_count": 1,
        "encore_bonus": 0,
        "free_ticket": 0,
        "idol_kind": 0,
        "play_mode": 1,  //0 = normal, 1 = coop with one other player
        "release_pamphlet_ids": [
            491
        ],
        "service_credit_count": 1
    },
    "protocol": "gameentry",
 */

public class GameEntryParam
{
    [JsonPropertyName("card_count")] 
    public int CardCount { get; set; }
    
    [JsonPropertyName("cracker_count")]
    public int CrackerCount { get; set; }
    
    [JsonPropertyName("credit_count")]
    public int CreditCount { get; set; }
    
    [JsonPropertyName("encore_bonus")]
    public int EncoreBonus { get; set; }
    
    [JsonPropertyName("free_ticket")]
    public int FreeTicket { get; set; }
    
    [JsonPropertyName("idol_kind")]
    public int IdolKind { get; set; }
    
    [JsonPropertyName("play_mode")]
    public int PlayMode { get; set; }
    
    [JsonPropertyName("release_pamphlet_ids")]
    public int[] ReleasePamphletIds { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("service_credit_count")]
    public int ServiceCreditCount { get; set; }
}