using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class HonorData
{
    [JsonPropertyName("m_honor_id")]
    public int HonorId { get; set; } 

    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; } 

    [JsonPropertyName("new")]
    public bool New { get; set; } 
}