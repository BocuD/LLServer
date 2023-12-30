using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class MemberCardData
{
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    
    [JsonPropertyName("m_card_member_id")]
    public int CardMemberId { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("print_rest")] 
    public int PrintRest { get; set; } = 1;

    [JsonPropertyName("new")]
    public bool New { get; set; }
    
    [JsonIgnore, NotMapped] public static int[] InitialMemberCards =
    {
        0,
        10011,
        20011,
        30011,
        40011,
        50011,
        60011,
        70011,
        80011,
        90011,
        0,
        110901,
        120901,
        130901,
        140901,
        150901,
        160901,
        170901,
        180901,
        190901,
        0,
        212000,
        222000
    };

    [JsonIgnore, NotMapped] public static int[] InitialMemorialCards =
    {
        0,
        1000,
        2000,
        3000,
        4000,
        5000,
        6000,
        7000,
        8000,
        9000,
        0,
        11100,
        12100,
        13100,
        14100,
        15100,
        16100,
        17100,
        18100,
        19100,
        0,
        21200,
        22200
    };
    
    [JsonIgnore, NotMapped] public static int[] YellRankAchieve20 =
    {
        0,
        10250,
        20250,
        30250,
        40250,
        50250,
        60250,
        70250,
        80250,
        90250,
        0,
        111310,
        121310,
        131310,
        141310,
        151310,
        161310,
        171310,
        181310,
        191310,
        0,
        0,
        0
    };

    [JsonIgnore, NotMapped] public static int[] YellRankAchieve80 =
    {
        0,
        10251,
        20251,
        30251,
        40251,
        50251,
        60251,
        70251,
        80251,
        90251,
        0,
        111311,
        121311,
        131311,
        141311,
        151311,
        161311,
        171311,
        181311,
        191311,
        0,
        0,
        0
    };

    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User? User { get; set; }
}