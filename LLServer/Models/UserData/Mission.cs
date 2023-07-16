using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class Mission
{
    [JsonPropertyName("mission_id")]
    public int MissionId { get; set; } 

    [JsonPropertyName("value")]
    public int Value { get; set; } 

    [JsonPropertyName("achieved")]
    public bool Achieved { get; set; }
}