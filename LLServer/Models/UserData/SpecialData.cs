using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class SpecialData
{
    [JsonPropertyName("idol_kind")]
    public int IdolKind { get; set; }

    [JsonPropertyName("special_id")]
    public int SpecialId { get; set; } 
}