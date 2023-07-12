using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

public class RequestBase
{
    [JsonPropertyName("protocol")] public string Protocol { get; set; } = string.Empty;
}