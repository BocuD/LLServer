using System.Text.Json.Serialization;

namespace LLServer;

public class BasicInfo
{
    [JsonPropertyName("response")]
    public required BasicInfoResponse Response { get; set; }
}

public class BasicInfoResponse
{
    [JsonPropertyName("base_url")]
    public required string BaseUrl { get; set; }

    [JsonPropertyName("download_url")]
    public required string DownloadUrl { get; set; }

    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("iv")]
    public required string Iv { get; set; }

    [JsonPropertyName("tenpo_index")]
    public required int TenpoIndex { get; set; }
}