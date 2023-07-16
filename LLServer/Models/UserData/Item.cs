using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class Item
{
    [JsonPropertyName("m_item_id")]
    public int ItemId { get; set; } 

    [JsonPropertyName("count")]
    public int Count { get; set; }
}