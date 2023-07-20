using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class MemberData
{
    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; }

    [JsonPropertyName("m_card_member_id")]
    public int CardMemberId { get; set; }

    [JsonPropertyName("yell_point")]
    public int YellPoint { get; set; }

    [JsonPropertyName("m_card_memorial_id")]
    public int CardMemorialId { get; set; }

    [JsonPropertyName("achieve_rank")]
    public int AchieveRank { get; set; }

    [JsonPropertyName("main")]
    public int Main { get; set; }

    [JsonPropertyName("camera")]
    public int Camera { get; set; }

    [JsonPropertyName("stage")]
    public int Stage { get; set; }

    [JsonPropertyName("select_count")]
    public int SelectCount { get; set; }

    [JsonPropertyName("new")]
    public bool New { get; set; } = true;
    
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User User { get; set; }
}