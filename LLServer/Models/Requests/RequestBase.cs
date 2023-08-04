using System.Text.Json;
using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

public class RequestBase
{
    [JsonPropertyName("protocol")]
    public string Protocol { get; set; } = string.Empty;

    [JsonPropertyName("param")]
    public JsonElement? Param { get; set; }
    
    [JsonPropertyName("sessionkey")]
    public string SessionKey { get; set; } = string.Empty;

    [JsonPropertyName("terminal")]
    public TerminalData Terminal { get; set; } = null!;
}

public class TerminalData
{
    [JsonPropertyName("tenpo_id")]
    public string TenpoId { get; set; } = string.Empty;
    
    [JsonPropertyName("tenpo_index")]
    public int TenpoIndex { get; set; } = 0;
    
    [JsonPropertyName("terminal_attrib")]
    public int TerminalAttrib { get; set; } = -1;
    
    [JsonPropertyName("terminal_id")]
    public string TerminalId { get; set; } = string.Empty;
}