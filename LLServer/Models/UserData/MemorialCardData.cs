using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class MemorialCardData
{
    [JsonPropertyName("m_card_memorial_id")]
    public int CardMemorialId { get; set; } 

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("print_rest")] 
    public int PrintRest { get; set; } = 1;

    [JsonPropertyName("select_count")]
    public int SelectCount { get; set; }

    [JsonPropertyName("talk_count")]
    public int TalkCount { get; set; }

    [JsonPropertyName("goal_count")]
    public int GoalCount { get; set; }

    [JsonPropertyName("new")]
    public bool New { get; set; } 
    
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User? User { get; set; }
}