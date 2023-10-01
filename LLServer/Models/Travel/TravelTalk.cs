using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

namespace LLServer.Models.Travel;

public class TravelTalk
{
    [JsonPropertyName("talk_id")] public int TalkId { get; set; }
    [JsonPropertyName("my_character_id")] public int MyCharacterId { get; set; }
    [JsonPropertyName("other_character_id")] public int OtherCharacterId { get; set; }
    
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User? User { get; set; }
}