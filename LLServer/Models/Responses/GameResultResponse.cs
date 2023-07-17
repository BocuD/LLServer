using System.Text.Json.Serialization;
using LLServer.Models.UserData;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace LLServer.Models.Responses;

public class GameResultResponse : ResponseBase
{
    [JsonPropertyName("musics")] public List<MusicData> Musics { get; set; } = new();
    [JsonPropertyName("stages")] public List<StageData> Stages { get; set; } = new();
    [JsonPropertyName("event_status")] public EventStatus EventStatus { get; set; } = new();
    [JsonPropertyName("event_rewards")] public List<EventReward> EventRewards { get; set; } = new();
    [JsonPropertyName("event_result")] public EventResult EventResult { get; set; } = new();
}