using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Responses;

public class PresentResponse : ResponseBase
{
    [JsonPropertyName("membercard")]
    public MemberCardData[] MemberCards { get; set; } = Array.Empty<MemberCardData>();
    
    [JsonPropertyName("skillcard")]
    public SkillCardData[] SkillCards { get; set; } = Array.Empty<SkillCardData>();
    
    [JsonPropertyName("memorialcard")]
    public MemorialCardData[] MemorialCards { get; set; } = Array.Empty<MemorialCardData>();
    
    [JsonPropertyName("item")]
    public Item[] Items { get; set; } = Array.Empty<Item>();
    
    [JsonPropertyName("mailbox")]
    public MailBoxItem[] MailBox { get; set; } = Array.Empty<MailBoxItem>();
}