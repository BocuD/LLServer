using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

namespace LLServer.Models.UserData;

public class CardFrame
{
    [JsonPropertyName("m_card_frame_id")]
    public int CardFrameId { get; set; }
    
    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; }
    
    [JsonPropertyName("new")]
    public bool New { get; set; }
    
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User? User { get; set; }
}