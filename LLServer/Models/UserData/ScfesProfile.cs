using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class ScfesProfile
{
    [JsonPropertyName("enable")]
    public bool Enable { get; set; } 
}