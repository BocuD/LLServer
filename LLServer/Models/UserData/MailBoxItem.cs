using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class MailBoxItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("attrib")]
    public int Attrib { get; set; }     //purpose unclear; 0 seems to just work™

    [JsonPropertyName("category")]
    public int Category { get; set; }   //1 might be member card (who knows); 2 seems to be skill card
    
    [JsonPropertyName("item_id")]
    public int ItemId { get; set; } 
    
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    //Database key
    [JsonIgnore, Key] public int DbId { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User? User { get; set; }
}