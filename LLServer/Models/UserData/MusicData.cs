using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class MusicData
{
    [JsonPropertyName("music_id")]
    public required int MusicId { get; set; }

    [JsonPropertyName("unlocked")]
    public required bool Unlocked { get; set; }

    [JsonPropertyName("new")]
    public required bool New { get; set; }
}