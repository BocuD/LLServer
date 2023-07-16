using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class MemberCardData
{
    [JsonPropertyName("m_card_member_id")]
    public int CardMemberId { get; set; } 

    [JsonPropertyName("count")]
    public int Count { get; set; } 

    [JsonPropertyName("print_rest")]
    public int PrintRest { get; set; } 

    [JsonPropertyName("new")]
    public bool New { get; set; }
}