using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Responses;

public class GetMemberCardResponse : ResponseBase
{
    [JsonPropertyName("membercard")]
    public MemberCardData[] MemberCard { get; set; } = new MemberCardData[0];
}