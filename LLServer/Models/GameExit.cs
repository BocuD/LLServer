using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models;

//todo: all the class arrays here (such as MemberCardData, SkillCardData, etc) should probably be int[] instead, but i am currently unsure and don't have a proper full request to test with
public class GameExit
{
    [JsonPropertyName("achievements")]
    public Achievement[] Achievements { get; set; } = Array.Empty<Achievement>();
    
    [JsonPropertyName("badges")]
    public Badge[] Badges { get; set; } = Array.Empty<Badge>();

    [JsonPropertyName("flags")]
    public string Flags { get; set; } = "";
    
    [JsonPropertyName("honors")]
    public HonorData[] Honors { get; set; } = Array.Empty<HonorData>();
    
    [JsonPropertyName("limited_achievements")]
    public LimitedAchievement[] LimitedAchievements { get; set; } = Array.Empty<LimitedAchievement>();
    
    [JsonPropertyName("lives")]
    public int[] Lives { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("membercard")]
    public MemberCardData[] MemberCards { get; set; } = Array.Empty<MemberCardData>();
    
    [JsonPropertyName("musics")]
    public int[] Musics { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("nameplates")]
    public int[] Nameplates { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skillcard")]
    public SkillCardData[] SkillCards { get; set; } = Array.Empty<SkillCardData>();
    
    [JsonPropertyName("stages")]
    public int[] Stages { get; set; } = Array.Empty<int>();
}