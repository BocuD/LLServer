using System.Text.Json.Serialization;

namespace LLServer.Models.Information;

public class ResourceInformation
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("category")]
    public int Category { get; set; }

    [JsonPropertyName("enable")]
    public bool Enable { get; set; }

    [JsonPropertyName("resource_id")]
    public string ResourceId { get; set; } = string.Empty;

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;
}