using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Responses;

public class PrintCardResponse : ResponseBase
{
    [JsonPropertyName("membercard")]
    public List<MemberCardData> MemberCards { get; set; } = new();

    [JsonPropertyName("skillcard")]
    public List<SkillCardData> SkillCards { get; set; } = new();

    [JsonPropertyName("memorialcard")] 
    public List<MemorialCardData> MemorialCards { get; set; } = new();
}