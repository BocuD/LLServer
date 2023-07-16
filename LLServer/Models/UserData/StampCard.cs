using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class StampCard
{
    [JsonPropertyName("stamp_card_id")]
    public int StampCardId { get; set; } 

    [JsonPropertyName("stamp_count")]
    public int StampCount { get; set; } 

    [JsonPropertyName("achieved")]
    public bool Achieved { get; set; } 

    //seems to go up to 9 characters (see .text:0000000140226E52)
    [JsonPropertyName("stamp_characters")]
    public int[] StampCharacters { get; set; } = new int[9];
}