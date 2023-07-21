using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Responses;

public class AchievementYellResponse : ResponseBase
{
    [JsonPropertyName("yell_achievements")]
    public YellAchievement[] YellAchievements { get; set; } = Array.Empty<YellAchievement>();
    
    [JsonPropertyName("membercard")]
    public MemberCardData[] MemberCards { get; set; } = Array.Empty<MemberCardData>();
    
    [JsonPropertyName("honors")]
    public HonorData[] Honors { get; set; } = Array.Empty<HonorData>();
    
    [JsonPropertyName("stages")]
    public StageData[] Stages { get; set; } = Array.Empty<StageData>();
    
    [JsonPropertyName("item")]
    public Item[] Items { get; set; } = Array.Empty<Item>();
}