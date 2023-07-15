using System.Text.Json.Serialization;

namespace LLServer.Models.Information;

public class MusicInformation
{
    [JsonPropertyName("music_id")]
    public int MusicId { get; set; }
}