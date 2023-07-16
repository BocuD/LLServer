using System.Text.Json.Serialization;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace LLServer.Models.Information;

public class MusicInformation
{
    [JsonPropertyName("music_id")]
    public int MusicId { get; set; }
}