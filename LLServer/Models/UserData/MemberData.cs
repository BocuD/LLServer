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

    [JsonPropertyName("yell_achieve_rank")]
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
    
    [JsonIgnore, NotMapped] public static int[] MemberIds =
    {
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        11,
        12,
        13,
        14,
        15,
        16,
        17,
        18,
        19,
        21,
        22
    };
    
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User? User { get; set; }
}