using System.Text.Json.Serialization;

namespace LLServer.Models.UserData;

public class SnapStamp
{
    [JsonPropertyName("stamp_id")]
    public int StampId { get; set; } = 0;

    [JsonPropertyName("stamp_nameplate_id")]
    public int StampNameplateId { get; set; } = 0;
}