using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

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
    
    [JsonIgnore, NotMapped] public static int[] InitialSkillCards =
    {
        //µ's
        1100,
        2100,
        3100,
        4100,
        5100,
        6100,
        7100,
        8100,
        9100,
        1200,
        2200,
        3200,
        4200,
        5200,
        6200,
        7200,
        8200,
        9200,
        1300,
        2300,
        3300,
        4300,
        5300,
        6300,
        7300,
        8300,
        9300,
        //Aqours
        11100,
        12100,
        13100,
        14100,
        15100,
        16100,
        17100,
        18100,
        19100,
        11200,
        12200,
        13200,
        14200,
        15200,
        16200,
        17200,
        18200,
        19200,
        11300,
        12300,
        13300,
        14300,
        15300,
        16300,
        17300,
        18300,
        19300,
        //Saint Snow
        21100,
        22100,
        21200,
        22200,
        21300,
        22300,
    };

    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User? User { get; set; }
}