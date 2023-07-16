using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class MailBoxItem
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("attrib")]
    public int Attrib { get; set; }

    [JsonPropertyName("category")]
    public int Category { get; set; } 

    [JsonPropertyName("item_id")]
    public int ItemId { get; set; } 
    
    [JsonPropertyName("count")]
    public int Count { get; set; } 
}