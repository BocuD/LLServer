using System.Text.Json.Serialization;

namespace LLServer.Models;

/*
{
    "param": {
        "achievements": [],
        "badges": [],
        "flags": "00000000010110001000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000",
        "honors": [],
        "limited_achievements": [],
        "lives": [
            10000,
            10001,
            10002,
            <etc>
        ],
        "membercard": [],
        "members": [],
        "memorialcard": [],
        "musics": [
            10,
            20,
            30,
            <etc>
        ],
        "nameplates": [],
        "skillcard": [],
        "stages": []
    },
    "protocol": "gameexit",
}
 */
public class GameExitParam
{
    [JsonPropertyName("achievements")]
    public int[] Achievements { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("badges")]
    public int[] Badges { get; set; } = Array.Empty<int>();

    [JsonPropertyName("flags")]
    public string Flags { get; set; } = "";
    
    [JsonPropertyName("honors")]
    public int[] Honors { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("limited_achievements")]
    public int[] LimitedAchievements { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("lives")]
    public int[] Lives { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("membercard")]
    public int[] MemberCards { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("members")]
    public int[] Members { get; set; } = Array.Empty<int>();

    [JsonPropertyName("memorialcard")] 
    public int[] MemorialCards { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("musics")]
    public int[] Musics { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("nameplates")]
    public int[] Nameplates { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skillcard")]
    public int[] SkillCards { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("stages")]
    public int[] Stages { get; set; } = Array.Empty<int>();
}