using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class SpecialItem
{
    [JsonPropertyName("idol_kind")]
    public int IdolKind { get; set; }

    [JsonPropertyName("special_id")]
    public int SpecialId { get; set; } 
    
    //todo: don't store this in a separate table
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User User { get; set; }
}