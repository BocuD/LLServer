using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class SkillCardData
{
    [JsonPropertyName("m_card_skill_id")]
    public int CardSkillId { get; set; } 

    [JsonPropertyName("skill_level")]
    public int SkillLevel { get; set; } 

    [JsonPropertyName("print_rest")]
    public int PrintRest { get; set; } 

    [JsonPropertyName("new")]
    public bool New { get; set; } 
}