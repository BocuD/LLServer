using System.Text.Json.Serialization;

namespace LLServer.Models.Requests.Gacha;

public class MemberGachaParam
{
    [JsonPropertyName("gacha_id")]
    public string GachaId { get; set; }
}