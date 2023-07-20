using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class TravelData
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("m_travel_pamphlet_id")]
    public int TravelPamphletId { get; set; } 

    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; } 

    [JsonPropertyName("m_card_memorial_id")]
    public int CardMemorialId { get; set; }

    //always seems to be 3 integers
    [JsonPropertyName("positions")]
    public int[] Positions { get; set; } = new int[3];

    [JsonPropertyName("last_landmark")]
    public int LastLandmark { get; set; } 

    [JsonPropertyName("modified")]
    public string Modified { get; set; } = "";
    
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User User { get; set; }
}