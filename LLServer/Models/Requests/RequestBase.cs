using System.Text.Json;
using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

public class RequestBase
{
    [JsonPropertyName("protocol")]
    public string Protocol { get; set; } = string.Empty;

    [JsonPropertyName("param")]
    public JsonElement? Param { get; set; }
}