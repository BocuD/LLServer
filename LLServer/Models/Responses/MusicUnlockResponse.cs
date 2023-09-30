using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Responses;

public class MusicUnlockResponse : ResponseBase
{
    [JsonPropertyName("musics")]
    public List<MusicData> Musics { get; set; } = new();
    
    [JsonPropertyName("lives")]
    public List<LiveData> Lives { get; set; } = new();
}