using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

/*
    "param": 
    {
        "id": "1",
        "sell": 0,
        "trade_type": 0
    },
    "protocol": "present",
 */
public class PresentParam
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("sell")]
    public int Sell { get; set; }
    
    [JsonPropertyName("trade_type")]
    public int TradeType { get; set; }
}