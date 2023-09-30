using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.Travel;

public class TravelPamphlet
{
    [JsonPropertyName("m_travel_pamphlet_id")]
    public int TravelPamphletId { get; set; } 

    [JsonPropertyName("round")]
    public int Round { get; set; } 

    [JsonPropertyName("total_talk_count")]
    public int TotalTalkCount { get; set; } 

    [JsonPropertyName("total_dice_count")]
    public int TotalDiceCount { get; set; }

    [JsonPropertyName("is_new")]
    public bool IsNew { get; set; }

    [JsonPropertyName("travel_ex_rewards")]
    public int[] TravelExRewards { get; set; } = Array.Empty<int>();
    
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User? User { get; set; }
}