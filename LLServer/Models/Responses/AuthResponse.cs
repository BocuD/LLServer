using System.Text.Json.Serialization;

namespace LLServer.Models.Responses;

public class AuthResponse : ResponseBase
{
    [JsonPropertyName("sessionkey")] public string SessionKey { get; set; } = string.Empty;

    [JsonPropertyName("user_id")] public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("blockseq")]
    public int BlockSequence { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("abnormal_end")]
    public int AbnormalEnd { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}