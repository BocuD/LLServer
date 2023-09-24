using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

/*
    "param": {
        "credit_count": 1,
        "idol_kind": 0,
        "service_credit_count": 1
    },
    "protocol": "gameentry.center",
 */

public class GameEntryCenterParam
{
    [JsonPropertyName("credit_count")]
    public int CreditCount { get; set; }
    
    [JsonPropertyName("idol_kind")]
    public int IdolKind { get; set; }
    
    [JsonPropertyName("service_credit_count")]
    public int ServiceCreditCount { get; set; }
}