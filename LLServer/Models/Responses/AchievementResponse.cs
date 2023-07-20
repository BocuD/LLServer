using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Responses;

public class AchievementResponse : ResponseBase
{
    [JsonPropertyName("achievements")]
    public Achievement[] Achievements { get; set; } = Array.Empty<Achievement>();
    
    [JsonPropertyName("record_books")]
    public AchievementRecordBook[] RecordBooks { get; set; } = Array.Empty<AchievementRecordBook>();
    
    [JsonPropertyName("honors")]
    public HonorData[] Honors { get; set; } = Array.Empty<HonorData>();
    
    [JsonPropertyName("stages")]
    public StageData[] Stages { get; set; } = Array.Empty<StageData>();
    
    [JsonPropertyName("item")]
    public Item[] Items { get; set; } = Array.Empty<Item>();
}