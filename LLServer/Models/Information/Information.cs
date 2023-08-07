using System.Text.Json.Serialization;

namespace LLServer.Models.Information;

public class Information
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("category")]
    public required int Category { get; set; }

    [JsonPropertyName("enable")]
    public required bool Enable { get; set; }

    [JsonPropertyName("order")]
    public required int Order { get; set; }

    [JsonPropertyName("display_satellite")]
    public required bool DisplaySatellite { get; set; }

    [JsonPropertyName("display_center")]
    public required int DisplayCenter { get; set; }

    /// <summary>
    /// Max length 30
    /// </summary>
    [JsonPropertyName("resource")]
    public required string Resource { get; set; }

    /// <summary>
    /// Max length 62
    /// </summary>
    [JsonPropertyName("image")]
    public required string Image { get; set; }

    /// <summary>
    /// Max length 254
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("start_datetime")]
    public required string StartDatetime { get; set; }

    [JsonPropertyName("end_datetime")]
    public required string EndDatetime { get; set; }
}