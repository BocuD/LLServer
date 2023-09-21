using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

/*
  "param": {
    "count": 1,
    "m_gacha_id": 2,
    "membercard_id": 70041,
    "mode": 0,
    "oshimen": -1,
    "position": 0,
    "rare": -1,
    "reason": 2
  },
  "protocol": "getmembercard",
}
 */

public class GetMemberCardParam
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("m_gacha_id")]
    public int GachaId { get; set; }
    
    [JsonPropertyName("membercard_id")]
    public int MemberCardId { get; set; }
    
    [JsonPropertyName("mode")]
    public int Mode { get; set; }
    
    [JsonPropertyName("oshimen")]
    public int Oshimen { get; set; }
    
    [JsonPropertyName("position")]
    public int Position { get; set; }
    
    [JsonPropertyName("rare")]
    public int Rare { get; set; }
    
    //reason 2 seems to be gacha pull
    [JsonPropertyName("reason")]
    public int Reason { get; set; }
}