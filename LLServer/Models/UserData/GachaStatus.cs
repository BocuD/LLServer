using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class GachaStatus
{
    [JsonPropertyName("m_gacha_id")]
    public int GachaId { get; set; } 

    [JsonPropertyName("usage_count")]
    public int UsageCount { get; set; } 

    [JsonPropertyName("idol_kind")]
    public int IdolKind { get; set; } 

    [JsonPropertyName("in_use")]
    public bool InUse { get; set; } 
}